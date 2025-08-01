let isActivePastButtton = false;
let cookieName = "PastPicture";


export function clearCookie(cookieName) {
    document.cookie = cookieName + "=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
}

export function SetCookie(cookieName, inputId) {
    var minites = 10;

    var expirationDate = new Date();
    expirationDate.setTime(expirationDate.getTime() + (minites * 60 * 1000)); // Convert minutes to milliseconds
    var expires = "expires=" + expirationDate.toUTCString();

    document.cookie = cookieName + "=" + inputId + "; " + expires;
}

export async function ActivePicturePastButton() {
    isActivePastButtton = true;

}

export function getCookie(cookieName) {
    var name = cookieName + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var cookieArray = decodedCookie.split(";");

    for (var i = 0; i < cookieArray.length; i++) {
        var cookie = cookieArray[i];
        while (cookie.charAt(0) === " ") {
            cookie = cookie.substring(1);
        }
        if (cookie.indexOf(name) === 0) {
            return cookie.substring(name.length, cookie.length);
        }
    }

    return null; // Cookie not found
}

document.addEventListener('paste', function (event) {
    if (isActivePastButtton) {

        var inputId = getCookie(cookieName);
        var inputFile = document.getElementById(inputId);

        inputFile.files = event.clipboardData.files;
        const changeEvent = new Event('change', { bubbles: true });
        inputFile.dispatchEvent(changeEvent);

        isActivePastButtton = false;
    }
});


