document.addEventListener("DOMContentLoaded", function () {
    const forms = document.querySelectorAll("form");

    forms.forEach(form => {
        const submitButton = form.querySelector("button[type='submit']");
        if (!submitButton) return;

        form.addEventListener("submit", function () {
            // Empêche le double envoi
            submitButton.disabled = true;
            submitButton.innerText = "En cours...";
        });
    });
});
