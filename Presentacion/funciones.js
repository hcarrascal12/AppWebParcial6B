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


function asignarDropdownListLibro(pValueAutor, pValueEdicion, pValueEditorial, pValueCategoria) {
    var cmbAutores = document.getElementById('ContentPlaceHolder1_cmbAutores');
    var cmbEdicion = document.getElementById('ContentPlaceHolder1_cmbEdicion');
    var cmbEditorial = document.getElementById('ContentPlaceHolder1_cmbEditorial'); 
    var cmbCategoria = document.getElementById('ContentPlaceHolder1_cmbCategoria'); 


    for (var i = 0; i < cmbAutores.options.length; i++) {
        var option = cmbAutores.options[i];
        if (option.value === pValueAutor) {
            option.selected = true;
            break;
        }
    }

    for (var i = 0; i < cmbEdicion.options.length; i++) {
        var option = cmbEdicion.options[i];
        if (option.value === pValueEdicion) {
            option.selected = true;
            break;
        }
    }

    for (var i = 0; i < cmbEditorial.options.length; i++) {
        var option = cmbEditorial.options[i];
        if (option.value === pValueEditorial) {
            option.selected = true;
            break;
        }
    }

    for (var i = 0; i < cmbCategoria.options.length; i++) {
        var option = cmbCategoria.options[i];
        if (option.value === pValueCategoria) {
            option.selected = true;
            break;
        }
    }
}