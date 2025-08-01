// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

setTimeout(() => {
    document.querySelector("#welcome-filter").remove();
}, 5000)


window.addEventListener('DOMContentLoaded', () => {
    const hasPlayed = localStorage.getItem('welcomePlayed');
    const welcome = document.getElementById('welcome-filter');

    if (!hasPlayed && welcome) {
        welcome.classList.add('animate');
        localStorage.setItem('welcomePlayed', 'true');
    } else {
        // L’animation ne se joue pas, on cache le filtre si besoin
        welcome.style.display = 'none';
    }
});