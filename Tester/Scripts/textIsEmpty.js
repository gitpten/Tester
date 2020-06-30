$(function () {
    document.frm.LogIn.disabled = true;
});

function textisEmpty() {
    document.frm.LogIn.disabled = true;

    if ($.trim(document.frm.UserName.value) != '') {
        document.frm.LogIn.disabled = false;
    }
}