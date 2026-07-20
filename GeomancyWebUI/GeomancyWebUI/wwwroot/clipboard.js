// Minimal clipboard + download helpers for Geofancy.

window.copyToClipboard = (text) => {
    if (navigator.clipboard && navigator.clipboard.writeText) {
        return navigator.clipboard.writeText(text).then(() => true).catch(err => {
            console.error('Failed to copy via Clipboard API:', err);
            return fallbackCopy(text);
        });
    }
    return Promise.resolve(fallbackCopy(text));
};

// Synchronous fallback for browsers that don't expose navigator.clipboard
// (older Safari, insecure contexts). Uses a transient hidden textarea +
// document.execCommand('copy'). Returns true/false.
function fallbackCopy(text) {
    try {
        const ta = document.createElement('textarea');
        ta.value = text;
        ta.setAttribute('readonly', '');
        ta.style.position = 'absolute';
        ta.style.left = '-9999px';
        document.body.appendChild(ta);
        ta.select();
        const ok = document.execCommand('copy');
        document.body.removeChild(ta);
        return ok;
    } catch (err) {
        console.error('Fallback copy failed:', err);
        return false;
    }
}

// Trigger a browser download of the given text as a file. Used by the JSON
// chart-export button. The blob URL is revoked after the click to avoid
// leaking memory across many exports in one session.
window.downloadTextFile = (filename, contents, mimeType) => {
    try {
        const type = mimeType || 'application/octet-stream';
        const blob = new Blob([contents], { type });
        const url = URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = filename || 'download.txt';
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);
        // Revoke on the next tick so Safari has time to start the download.
        setTimeout(() => URL.revokeObjectURL(url), 0);
        return true;
    } catch (err) {
        console.error('Failed to trigger file download:', err);
        return false;
    }
};

/** localStorage helpers for Blazor (e.g. one-time mobile tips). */
window.geofancyReadStorage = (key) => {
    try {
        return localStorage.getItem(key);
    } catch {
        return null;
    }
};

window.geofancyWriteStorage = (key, value) => {
    try {
        localStorage.setItem(key, value);
        return true;
    } catch {
        return false;
    }
};

window.geofancyRemoveStorage = (key) => {
    try {
        localStorage.removeItem(key);
        return true;
    } catch {
        return false;
    }
};

/** Open any collapsed <details> ancestors so the target section is visible. */
function geofancyOpenDetailsAncestors(el) {
    let node = el.parentElement;
    while (node) {
        if (node.tagName === 'DETAILS' && !node.open) {
            node.open = true;
        }
        node = node.parentElement;
    }
    const nested = el.querySelector('details:not([open])');
    if (nested) {
        nested.open = true;
    }
}

/**
 * Find the element that actually scrolls for figure detail (desktop: .figure-detail-panel,
 * mobile: .mobile-panel-body when panel CSS moves overflow to the parent).
 */
function geofancyFindFigureDetailScroller(targetEl) {
    const panel = targetEl.closest('.figure-detail-panel');
    let node = targetEl.parentElement;

    while (node) {
        const style = window.getComputedStyle(node);
        const overflowY = style.overflowY;
        const scrollable =
            (overflowY === 'auto' || overflowY === 'scroll' || overflowY === 'overlay') &&
            node.scrollHeight > node.clientHeight + 2;

        if (scrollable) {
            const sticky =
                panel?.querySelector('.detail-sticky-tabs') ||
                node.querySelector('.detail-sticky-tabs');
            const backBar = targetEl.closest('.mobile-panel')?.querySelector('.mobile-panel-back');
            return { scroller: node, sticky, backBar };
        }

        node = node.parentElement;
    }

    if (panel) {
        const style = window.getComputedStyle(panel);
        const overflowY = style.overflowY;
        const scrollable =
            (overflowY === 'auto' || overflowY === 'scroll' || overflowY === 'overlay') &&
            panel.scrollHeight > panel.clientHeight + 2;
        if (scrollable) {
            return {
                scroller: panel,
                sticky: panel.querySelector('.detail-sticky-tabs'),
                backBar: null
            };
        }
    }

    return null;
}

/** Scroll a panel section into view without triggering Blazor router navigation. */
window.geofancyScrollToId = (id) => {
    const el = document.getElementById(id);
    if (!el) {
        return false;
    }

    geofancyOpenDetailsAncestors(el);

    const scrollTarget = geofancyFindFigureDetailScroller(el);
    if (scrollTarget?.scroller) {
        const { scroller, sticky, backBar } = scrollTarget;
        const stickyH = sticky ? sticky.getBoundingClientRect().height : 0;
        const backH =
            backBar && scroller.closest('.mobile-panel-body')
                ? backBar.getBoundingClientRect().height
                : 0;
        const scrollerRect = scroller.getBoundingClientRect();
        const elRect = el.getBoundingClientRect();
        const delta = elRect.top - scrollerRect.top + scroller.scrollTop - stickyH - backH - 8;
        scroller.scrollTo({ top: Math.max(0, delta), behavior: 'smooth' });
    } else {
        el.scrollIntoView({ behavior: 'smooth', block: 'start' });
    }

    return true;
};

/** Reset scroll position on a scrollable panel element. */
window.geofancyScrollElementToTop = (el) => {
    if (!el) {
        return false;
    }

    el.scrollTop = 0;
    const mobileBody = el.closest('.mobile-panel-body');
    if (mobileBody) {
        mobileBody.scrollTop = 0;
    }

    return true;
};

/** Keep desktop mothers cast stack height/top locked to the shield chart box. */
window.geofancyMothersCastSync = {
    _sessions: new Map(),
    start: function (key, chartRoot, castStack, castAlign) {
        this.stop(key);
        if (!chartRoot || !castStack) {
            return;
        }

        var chartEl = chartRoot.classList && chartRoot.classList.contains('shield-chart-container')
            ? chartRoot
            : chartRoot.querySelector('.shield-chart-container');
        if (!chartEl) {
            return;
        }

        var apply = function () {
            var chartRect = chartEl.getBoundingClientRect();
            var h = Math.round(chartRect.height);
            if (h <= 40) {
                return;
            }

            castStack.style.height = h + 'px';
            castStack.style.maxHeight = h + 'px';
            castStack.style.minHeight = h + 'px';

            if (castAlign) {
                var alignRect = castAlign.getBoundingClientRect();
                var offset = Math.round(chartRect.top - alignRect.top);
                castStack.style.marginTop = Math.max(0, offset) + 'px';
            }
        };

        var ro = new ResizeObserver(function () { apply(); });
        ro.observe(chartEl);
        if (castAlign) {
            ro.observe(castAlign);
        }
        window.addEventListener('resize', apply);
        apply();
        this._sessions.set(key, { ro: ro, castStack: castStack, apply: apply });
    },
    stop: function (key) {
        var session = this._sessions.get(key);
        if (!session) {
            return;
        }

        session.ro.disconnect();
        window.removeEventListener('resize', session.apply);
        session.castStack.style.height = '';
        session.castStack.style.maxHeight = '';
        session.castStack.style.minHeight = '';
        session.castStack.style.marginTop = '';
        this._sessions.delete(key);
    }
};
