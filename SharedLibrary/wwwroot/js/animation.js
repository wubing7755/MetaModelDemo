// 动画基础实现
function showElement(element, duration) {
    element.style.display = 'block';
    element.style.transition = `opacity ${duration}ms`;
    element.style.opacity = '1';
}

function hideElement(element, duration) {
    element.style.opacity = '0';
    setTimeout(() => element.style.display = 'none', duration);
}

// 淡入淡出动画
function fadeIn(element, duration) {
    element.style.display = 'flex';
    element.style.transition = `opacity ${duration}ms ease-out`;
    element.style.opacity = '1';
}

function fadeOut(element, duration) {
    element.style.transition = `opacity ${duration}ms ease-in`;
    element.style.opacity = '0';
    element.style.display = 'none';
}


// 缩放动画
function zoomIn(element, duration) {
    element.style.transform = 'scale(0)';
    element.style.display = 'block';
    setTimeout(() => element.style.transform = 'scale(1)', 10);
}

function zoomOut(element, duration) {
    element.style.transform = 'scale(0)';
    setTimeout(() => element.style.display = 'none', duration);
}

export {showElement, hideElement, fadeIn, fadeOut, zoomIn, zoomOut};