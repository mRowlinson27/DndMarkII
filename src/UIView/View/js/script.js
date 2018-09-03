function ValidateInts(source) {
    if (!isNumeric(source.value)) {
        alert('Invalid value');
        source.value = source.oldvalue;
    }
}

function isNumeric(value) {
    return /^-{0,1}\d+$/.test(value);
}