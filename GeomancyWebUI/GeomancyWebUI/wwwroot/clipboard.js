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
