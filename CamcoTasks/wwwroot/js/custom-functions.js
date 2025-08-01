reInitOnDOMChange();
function initializeHoverImageSwap() {
    const buttons = document.querySelectorAll(".tool-button");

    buttons.forEach(button => {
        const label = button.querySelector(".text-wrapper-37");
        const imagePath = button.getAttribute("data-img");

        if (!imagePath || !label) return;

        // Prevent adding multiple images if function runs more than once
        if (button.querySelector(".preview-inline-img")) return;

        // Create hover image
        const imgElement = document.createElement("img");
        imgElement.src = `/img/${imagePath}`;
        imgElement.classList.add("preview-inline-img");
        imgElement.style.opacity = "0";
        button.appendChild(imgElement);

        // Hover events: show image, hide text
        button.addEventListener("mouseenter", () => {
            label.style.visibility = "hidden";
            imgElement.classList.add("fade-in");
            imgElement.style.opacity = "1";
        });

        button.addEventListener("mouseleave", () => {
            label.style.visibility = "visible";
            imgElement.classList.remove("fade-in");
            imgElement.style.opacity = "0";
        });
    });
}

function scrollToTopOnNavigation() {
    let lastPath = location.pathname;

    new MutationObserver(() => {
        if (location.pathname !== lastPath) {
            lastPath = location.pathname;
            window.scrollTo({ top: 0, behavior: 'smooth' });
        }
    }).observe(document.body, { childList: true, subtree: true });
}
function initializeTestimonialSlider() {
    const carouselElement = document.querySelector('#testimonialCarousel');
    if (!carouselElement || !bootstrap?.Carousel) return;

    const carousel = new bootstrap.Carousel(carouselElement, {
        interval: false,
        ride: false,
        wrap: true
    });

    const prevButtons = document.querySelectorAll('[data-bs-target="#testimonialCarousel"].carousel-control-prev');
    const nextButtons = document.querySelectorAll('[data-bs-target="#testimonialCarousel"].carousel-control-next');

    prevButtons.forEach(btn => {
        btn.addEventListener('click', () => carousel.prev());
    });

    nextButtons.forEach(btn => {
        btn.addEventListener('click', () => carousel.next());
    });
}

window.addEventListener('load', () => {
    scrollToTopOnNavigation();
    initializeHoverImageSwap();
})
function reInitOnDOMChange() {
    const observer = new MutationObserver(() => {
        initializeHoverImageSwap();
        initializeTestimonialSlider();
        //scrollToTopOnDomChange();
    });

    observer.observe(document.body, { childList: true, subtree: true });
}

window.scrollToTop = () => {
    //console.log("scrollToTop() called");
    window.scrollTo({ top: 0, behavior: 'smooth' });
};


window.trackEvent = function (eventName, parameters) {
    if (!window.dataLayer) return;

    // Determine device type by screen width
    const screenWidth = window.innerWidth;
    let deviceType = "desktop";
    if (screenWidth < 768) deviceType = "mobile";
    else if (screenWidth <= 1024) deviceType = "tablet";

    const urlParams = new URLSearchParams(window.location.search);


    // Determine if we’re in a dev/staging environment
    const isDev =
        window.location.hostname.includes("localhost") ||
        window.location.hostname.includes("staging");

    const enrichedParams = {
        event: eventName,

        // Conditionally add debug mode
        ...(isDev ? { debug_mode: true } : {}),

        // Device info
        device_screen_width: screenWidth,
        device_screen_height: window.innerHeight,
        device_type: deviceType,
        device_pixel_ratio: window.devicePixelRatio,
        device_is_touch: 'ontouchstart' in window,

        // Environment/browser info
        env_language: navigator.language,
        env_user_agent: navigator.userAgent,
        env_platform: navigator.platform,
        env_timezone: Intl.DateTimeFormat().resolvedOptions().timeZone,

        // Page info
        page_referrer: document.referrer,
        page_url: window.location.href,
        page_path: window.location.pathname,
        page_query_string: window.location.search,
        page_hash: window.location.hash,
        page_host: window.location.host,

        // UTM parameters
        utm_source: urlParams.get("utm_source"),
        utm_medium: urlParams.get("utm_medium"),
        utm_campaign: urlParams.get("utm_campaign"),
        utm_term: urlParams.get("utm_term"),
        utm_content: urlParams.get("utm_content"),

        // Custom parameters
        ...parameters
    };

    window.dataLayer.push(enrichedParams);
};

let maxScrollTracked = 0;
window.addEventListener("scroll", () => {
    const scrollTop = window.scrollY;
    const docHeight = document.documentElement.scrollHeight - window.innerHeight;
    const percent = Math.round((scrollTop / docHeight) * 100);

    const breakpoints = [25, 50, 75, 100];
    const next = breakpoints.find(p => percent >= p && p > maxScrollTracked);

    if (next) {
        window.trackEvent("scroll_depth", { percent_scrolled: next });
        maxScrollTracked = next;
    }
});
