// Jacob Kukuk Portfolio - Interactive Scripts

// Boot Sequence Logic
const bootText = [
    "INITIALIZING KUKUK_BIOS v1.0...",
    "CHECKING MEMORY... 64MB OK",
    "LOADING DRIVERS... C# READY",
    "LOADING DRIVERS... SQL READY",
    "LOADING DRIVERS... AZURE READY",
    "MOUNTING FILE SYSTEM...",
    "ESTABLISHING UPLINK TO LAS VEGAS...",
    "SYSTEM READY."
];

const bootScreen = document.getElementById('boot-screen');
const textContainer = document.getElementById('boot-text');

let lineIndex = 0;

function typeLine() {
    if (lineIndex < bootText.length) {
        const line = document.createElement('div');
        line.textContent = "> " + bootText[lineIndex];
        textContainer.appendChild(line);
        lineIndex++;
        
        const delay = Math.random() * 300 + 100;
        setTimeout(typeLine, delay);
    } else {
        setTimeout(() => {
            bootScreen.style.opacity = '0';
            bootScreen.style.transition = 'opacity 0.5s ease';
            setTimeout(() => {
                bootScreen.style.display = 'none';
            }, 500);
        }, 800);
    }
}

// Start boot sequence on load
window.addEventListener('DOMContentLoaded', typeLine);

// Score Counter Simulation
const scoreEl = document.getElementById('score');
if (scoreEl) {
    setInterval(() => {
        let score = parseInt(scoreEl.innerText);
        score += Math.floor(Math.random() * 10);
        scoreEl.innerText = score.toString().padStart(6, '0');
    }, 2000);
}

// Smooth scroll for navigation links
document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener('click', function (e) {
        e.preventDefault();
        const target = document.querySelector(this.getAttribute('href'));
        if (target) {
            target.scrollIntoView({
                behavior: 'smooth',
                block: 'start'
            });
            // Update URL without scrolling
            history.pushState(null, null, this.getAttribute('href'));
        }
    });
});

// Lazy load images when they come into viewport
if ('IntersectionObserver' in window) {
    const imageObserver = new IntersectionObserver((entries, observer) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                const img = entry.target;
                if (img.dataset.src) {
                    img.src = img.dataset.src;
                    img.removeAttribute('data-src');
                }
                observer.unobserve(img);
            }
        });
    });

    document.querySelectorAll('img[data-src]').forEach(img => {
        imageObserver.observe(img);
    });
}

// Handle ESC key to scroll to top (like "EXIT")
document.addEventListener('keydown', (e) => {
    if (e.key === 'Escape') {
        window.scrollTo({ top: 0, behavior: 'smooth' });
    }
});

// Performance: Reduce animation on low-end devices
if (navigator.hardwareConcurrency && navigator.hardwareConcurrency < 4) {
    document.documentElement.classList.add('reduce-animations');
}
