// Minimal clipboard functionality
window.copyToClipboard = (text) => {
    return navigator.clipboard.writeText(text).then(() => {
        return true;
    }).catch(err => {
        console.error('Failed to copy to clipboard:', err);
        return false;
    });
};

