

function validate() {
    var formAdd = document.getElementById("formAddUser");
    var res = true;
    if (formAdd.username.value.length == 0) {
        res = false;
        document.getElementById("usernameWarning").innerHTML = "không được để trống";
    } else {
        document.getElementById("usernameWarning").innerHTML = "";
    }

    if (formAdd.name.value.trim().length == 0) {
        res = false;
        document.getElementById("nameWarning").innerHTML = "không được để trống";
    } else {
        document.getElementById("nameWarning").innerHTML = "";
    }

    if (formAdd.id.value.trim().length == 0) {
        res = false;
        document.getElementById("idWarning").innerHTML = "không được để trống";
    } else {
        document.getElementById("idWarning").innerHTML = "";
    }
    
    if (formAdd.dayOfBirth.value.length == 0) {
        res = false;
        document.getElementById("dateOfBirthWarning").innerHTML = "chưa chọn ngày";
    } else {
        document.getElementById("dateOfBirthWarning").innerHTML = "";
    } 


    if (formAdd.email.value.length == 0) {
        res = false;
        document.getElementById("emailWarning").innerHTML = "không được để trống";
    } else {
        if (!validateEmail(formAdd.email.value)) {
            res = false;
            document.getElementById("emailWarning").innerHTML = "không đúng định dạng";
        }else
            document.getElementById("emailWarning").innerHTML = "";
    } 

    if (formAdd.phone.value.length == 0) {
        res = false;
        document.getElementById("phoneWarning").innerHTML = "không được để trống";
    } else {
        if (!isInt(formAdd.phone.value)) {
            res = false;
            document.getElementById("phoneWarning").innerHTML = "không đúng định dạng";
        }else
            document.getElementById("phoneWarning").innerHTML = "";
    } 

    if (formAdd.grade)
        if (!isInt(formAdd.grade.value)) {
            res = false;
            document.getElementById("gradeWarning").innerHTML = "không đúng định dạng";
        }else {
            document.getElementById("gradeWarning").innerHTML = "";
        }

    return res;
}

function validateEmail(email) {
    var re = /\S+@\S+\.\S+/;
    return re.test(email);
}
function isInt(value) {
    var er = /^[0-9]+$/;
    return er.test(value);
}