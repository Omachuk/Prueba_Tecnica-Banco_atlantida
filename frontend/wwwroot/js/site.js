// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function formaroFecha(fechaOriginal) {
    const date = new Date(fechaOriginal);
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0'); // Sumar 1 al mes ya que es base 0
    const day = String(date.getDate()).padStart(2, '0');
    const hour = String(date.getHours()).padStart(2, '0');
    const minute = String(date.getMinutes()).padStart(2, '0');
    const second = String(date.getSeconds()).padStart(2, '0');

    const fechaFormateada = `${day}/${month}/${year} ${hour}:${minute}:${second}`;

    return fechaFormateada;
}

function validarNum(event, fieldName, maxLength) {
    const inputElement = document.getElementById(fieldName);
    const inputValue = inputElement.value;

    if (event.key === 'Enter') {
        return true;
    }

    if (!/[0-9]/.test(event.key)) {
        event.preventDefault();
        return false;
    }

    if (inputValue.length === maxLength) {
        event.preventDefault();
        return false;
    }

    return true;
}
