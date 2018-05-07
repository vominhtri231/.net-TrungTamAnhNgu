

function validate() {
    var formAdd = document.getElementById("formAddClass");
    var res = true;
    if (formAdd.id.value.length == 0) {
        res = false;
        document.getElementById("idWarning").innerHTML = "không được để trống";
    } else {
        document.getElementById("idWarning").innerHTML = "";
    }
    
    if (formAdd.name.value.trim().length == 0) {
        res = false;
        document.getElementById("nameWarning").innerHTML = "không được để trống";
    } else {
        document.getElementById("nameWarning").innerHTML = "";
    }

    if (formAdd.teacherUsername.value.trim().length == 0) {
        res = false;
        document.getElementById("teacherUsernameWarning").innerHTML = "Chưa chọn giáo viên";
    } else {
        document.getElementById("teacherUsernameWarning").innerHTML = "";
    }

    if (formAdd.charge.value.length == 0) {
        res = false;
        document.getElementById("chargeWarning").innerHTML = "không được để trống";
    } else {
        document.getElementById("chargeWarning").innerHTML = "";
    }

    return res;
}