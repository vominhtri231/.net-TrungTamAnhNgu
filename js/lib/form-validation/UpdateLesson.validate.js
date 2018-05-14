

function validate() {
    var formAdd = document.getElementById("formUpdateLesson");
    var res = true;

    if (formAdd.classNumber.value.trim().length == 0) {
        res = false;
        document.getElementById("classNumberWarning").innerHTML = "không được để trống";
    } else {
        if (!isInt(formAdd.classNumber.value)) {
            res = false;
            document.getElementById("classNumberWarning").innerHTML = "không đúng định dạng ";
        } else
            document.getElementById("classNumberWarning").innerHTML = "";
    }

    if (formAdd.content.value.trim().length == 0) {
        res = false;
        document.getElementById("contentWarning").innerHTML = "không được để trống";
    } else {
        document.getElementById("contentWarning").innerHTML = "";
    }

    if (formAdd.location.value.trim().length == 0) {
        res = false;
        document.getElementById("locationWarning").innerHTML = "không được để trống";
    } else {
        document.getElementById("locationWarning").innerHTML = "";
    }


    if (formAdd.day.value.trim().length == 0) {
        res = false;
        document.getElementById("dayWarning").innerHTML = "không được để trống";
    } else {
        document.getElementById("dayWarning").innerHTML = "";
    }

   

    return res;
}

function isInt(value) {
    var er = /^[0-9]+$/;
    return er.test(value);
}