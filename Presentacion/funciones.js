function executeAlertify(status, message) {
    if (status == true) {
        alertify.success(message);
    } else {
        alertify.error(message);
    }
}

function showModal(pModal) {
    const myModal = new bootstrap.Modal(pModal);
    myModal.show();
}

function closeModal(pModal) {
    const myModal = new bootstrap.Modal(pModal);
    myModal.hide();
}

function asignarDropdownList(pIdTag, pValue) {
    var comboBox = document.getElementById(pIdTag);


    for (var i = 0; i < comboBox.options.length; i++) {
        var option = comboBox.options[i];
        if (option.value === pValue) {
            option.selected = true;
            break;
        }
    }
}