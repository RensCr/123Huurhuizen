// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function getUserIdFromCookie(cookieName) {
    const cookies = `; ${document.cookie}`;
    const cookieParts = cookies.split(`; ${cookieName}=`);

    if (cookieParts.length === 2) {
        const cookieValue = cookieParts.pop().split(';').shift();

        if (cookieValue) {
            // JWT-token decoderen
            const base64Url = cookieValue.split('.')[1];
            const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
            const decodedData = JSON.parse(window.atob(base64));

            // ID uit de payload halen en retourneren
            console.log(decodedData.Id)
            return decodedData.Id;

        }
    }

    console.log(`${cookieName} niet gevonden in cookies.`);
    return null; // Geen gebruikers-ID gevonden, retourneer null
}