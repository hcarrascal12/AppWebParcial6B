function executeAlertify(status, message) {
    if (status == true) {
        alertify.success(message);
    } else {
        alertify.error(message);
    }
}
