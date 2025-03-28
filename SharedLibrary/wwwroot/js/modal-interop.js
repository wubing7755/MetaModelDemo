function lockScroll() {
    document.body.style.overflow = 'hidden';
    document.body.style.paddingRight =
        window.innerWidth - document.documentElement.clientWidth + 'px';
}

function unlockScroll() {
    document.body.style.overflow = '';
    document.body.style.paddingRight = '';
}

// 添加防抖处理
let scrollLockCount = 0;

function safeLockScroll() {
    if (scrollLockCount++ === 0) {
        lockScroll();
    }
}

function safeUnlockScroll() {
    if (--scrollLockCount === 0) {
        unlockScroll();
    }
}

export {lockScroll, unlockScroll, safeLockScroll, safeUnlockScroll};
