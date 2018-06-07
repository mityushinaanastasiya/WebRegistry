function DeleteBlog() {
    var request = new XMLHttpRequest();
    select = document.getElementById("deleteDiv");
    id = select.options[select.selectedIndex].value;
    url = "/api/blogsapi/" + id;
    request.open("DELETE", url, false);
    request.send();
}

function CreateBlog() {
    urlText = document.getElementById("createDiv").value;

    var xmlhttp = new XMLHttpRequest(); // new HttpRequest instance
    xmlhttp.open("POST", "/api/blogsapi/");
    xmlhttp.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    xmlhttp.send(JSON.stringify({
        url: urlText
    }));
}

function LoadDoctors() {
    let myObj, i, x = "";
    var request = new XMLHttpRequest();
    request.open("GET", "/api/doctorsfull/", false);
    request.send();

    myObj = JSON.parse(request.responseText);
    for (i in myObj) {
        x += "<div class=\"row padding-doctor\">";
        x += "<div class=\"card border-primary shadow-doctor\" style=\"width: 100%;\">";
        x += "<div class=\"card-header\">" + myObj[i].specialty.specialtyName + "</div>";
        x += "<div class=\"card-body text-primary\">";
        x += "<h5 class=\"card-title\">" + myObj[i].surname + " " + myObj[i].name + " " + myObj[i].patronymic + "</h5>";
        x += "<div class=\"d-flex bd-highlight\">";
        x += "<div class=\"p-2 flex-grow-1 bd-highlight\">";
        x += "<p class=\"card-text\">Больница: " + myObj[i].hospital.hospitalName + "</p>";
        x += "</div>";
        x += "<div class=\"p-2 bd-highlight\">";
        x += "<button type=\"button\" class=\"btn btn-outline-info\" onclick=\"LoadAppointment(" + myObj[i].doctorId + ");\">Посмотреть расписание</button>";
        x += "</div>";
        x += "</div>";
        x += "</div>";
        x += "<div class=\"tabel-appointment\" id=\"t-appointment" + myObj[i].doctorId + "\">";
        x += "</div>";
        x += "</div>";
        x += "</div>";
        document.getElementById("doctors").innerHTML = x;
    }
}
//КОРОЧЕЕЕЕ ОНА ИЩЕТ БЛИЖАЙЩИЙ ПОНЕДЕЛЬНИЕ
function LastMonday() {
    let now = new Date();
    let toDay = now.getDay();
    let del = 0;
    if (toDay == 0) {
        toDay = 7;
    }
    del = toDay - 1;
    now.setDate(now.getDate() - del);
    return now;
}

var min = [24, 0];
var max = [0, 0];
var countApp = 0;
//кОрОчЧЕ ОНА БУДЕТ ПОНИМАТЬ С КОКОГО ПО КАКОЕ ВРЕМЯ ЕСТЬ СВОБОДНЫЕ ЗАПИСИ
function TimeOfReceipt(now, doctorID) {
    var request = new XMLHttpRequest();
    request.open("GET", "/api/appointments/", false);
    request.send();
    let myObj = JSON.parse(request.responseText);
    min = [24, 0];
    max = [0, 0];
    countApp = 0;
    for (let i in myObj) {
        let year = new Date(myObj[i].dataTime).getFullYear();
        let month = new Date(myObj[i].dataTime).getMonth();
        let day = new Date(myObj[i].dataTime).getDate();
        if (myObj[i].doctorId == doctorID && myObj[i].patientId == null && year == now.getFullYear() && month == now.getMonth() && day == now.getDate()) {
            var hours = new Date(myObj[i].dataTime).getHours();
            var minutes = new Date(myObj[i].dataTime).getMinutes();
            countApp++;
            if (min[0] > hours) {
                min[0] = hours;
            }
            if (max[0] < hours) {
                max[0] = hours;
            }
            if (min[1] > minutes) {
                min[1] = minutes;
            }
            if (max[1] < minutes) {
                max[1] = minutes;
            }
        }

    }
}

var monthArr = [
    'Января',
    'Февраля',
    'Марта',
    'Апреля',
    'Мая',
    'Июня',
    'Июля',
    'Августа',
    'Сентября',
    'Ноября',
    'Декабря'
];
var weekArr = [
    'Пн',
    'Вт',
    'Ср',
    'Чт',
    'Пт',
    'Сб',
    'Вс'
];

function LoadAppointment(doctorID) {
    if (document.getElementById("t-appointment" + doctorID).innerHTML == "") {
        var dateTitel = LastMonday();
        var dateApp = LastMonday();
        var html = "";
        html += "<div class=\"table-responsive-xl\">";
        html += "<table class=\"table tabel-center\">";
        for (let i = 0; i < 3; i++) {
            html += "<tr class=\"tabel-data-row\">";
            for (let j = 0; j < 7; j++) {
                html += "<td scope=\"col\">";
                html += weekArr[j] + "<br>" + dateTitel.getDate() + " " + monthArr[dateTitel.getMonth()];
                html += "</td>";
                dateTitel.setDate(dateTitel.getDate() + 1);
            }
            html += "</tr>";
            html += "<tr>";
            for (let j = 0; j < 7; j++) {
                TimeOfReceipt(dateApp, doctorID);
                html += "<td>";
                if (countApp > 0) {
                    html += "<button type=\"button\" class=\"btn btn-success\"  data-toggle=\"modal\" data-target=\".bd-example-modal-lg\" onclick=\"AppointmentList(" + doctorID + ",\'" + dateApp.toISOString() + "\')\">";
                    html += DateParse(min[0], min[1]);
                    html += " - "
                    html += DateParse(max[0], max[1]);
                    html += "<br>";
                    html += "Свободно: " + "<span class=\"badge badge-light\">" + countApp + "</span>";
                    html += "</button>";
                } else {
                    html += "<button type=\"button\" class=\"btn btn-danger\" disabled>";
                    html += "Свободно: <span class=\"badge badge-light\">0</span>";
                    html += "</button>";
                }
                html += "</td>";
                dateApp.setDate(dateApp.getDate() + 1);
            }
            html += "</tr>";
        }
        html += "</table>";
        html += "</div>";
        document.getElementById("t-appointment" + doctorID).innerHTML = html;
    } else {
        document.getElementById("t-appointment" + doctorID).innerHTML = "";
    }
}

function DateParse(hours, minutes) {
    let str = "";
    if (hours > 9) {
        str += hours + ":";
    } else {
        str += "0" + hours + ":";
    }
    if (minutes > 9) {
        str += minutes;
    } else {
        str += "0" + minutes;
    }
    return str;
}

function AppointmentList(doctorID, dateNow) {
    var now = new Date(dateNow);
    let patientId = GetPatientId();
    let html = "";
    html += "<table class=\"table tabel-center tabel-padding\">";
    html += "<thead>";
    html += "<tr>";
    html += "<th class=\"table-info\" width=\"10%\" scope=\"col\">Часы<br>приёма</th>";
    html += "<th class=\"table-success\" width=\"90%\" scope=\"col\" colspan=\"4\">Пн<br>1 Мая<br>12:00 - 16:00</th>";
    html += "</tr>";
    html += "</thead>";
    html += "<tbody>";
    for (let i = 0; i < 7; i++) {
        html += "<tr>";
        html += "<th scope=\"row\">" + DateParse(10 + i, 0) + "</th>";
        for (let j = 0; j < 4; j++) {
            html += "<td>";
            html += "<button id=\"appointmen-time" + (10 + i) + "-" + (j * 15) + "\" type=\"button\" class=\"btn btn-warning btn-block\" disabled>" + DateParse(10 + i, 0 + (j * 15)) + "</button>"
            html += "</td>";
        }
        html += "</tr>";
    }
    html += "</tbody>";
    html += "</table>";
    html += "<div id=\"yes-no\">";
    html += "</div>";
    document.getElementById("t-appointment-time").innerHTML = html;
    let request = new XMLHttpRequest();
    request.open("GET", "/api/AppointmentsForDoctor/" + doctorID, false);
    request.send();
    let myObj = JSON.parse(request.responseText);
    for (const i in myObj) {
        if (myObj.hasOwnProperty(i)) {
            const element = myObj[i];
            let dataElem = new Date(element.dataTime);
            if (dataElem.getFullYear() == now.getFullYear() && dataElem.getMonth() == now.getMonth() && dataElem.getDate() == now.getDate()) {
                if (element.patientId == null) {
                    document.getElementById("appointmen-time" + dataElem.getHours() + "-" + dataElem.getMinutes()).className = "btn btn-success btn-block active";
                    document.getElementById("appointmen-time" + dataElem.getHours() + "-" + dataElem.getMinutes()).disabled = false;
                    document.getElementById("appointmen-time" + dataElem.getHours() + "-" + dataElem.getMinutes()).onclick = function () {
                        ActionsAppointment(element.appointmentId, element.doctorId, GetPatientId(), element.dataTime, element.daysOfWeekId);
                    }
                } else {
                    document.getElementById("appointmen-time" + dataElem.getHours() + "-" + dataElem.getMinutes()).className = "btn btn-danger btn-block";
                }
            }

        }
    }
    if (addMode == false) {
        setDropdowmMenu(doctorID, dateNow, patientId);
    }
}

function setCloseButtomAdd(doctorID, dateNow, patientId) {
    var html2 = "";
    html2 += "<button type=\"button\" class=\"btn btn-outline-info\" onclick=\"UNSetAddMode(" + doctorID + ",\'" + dateNow + "\'," + patientId + ");\">Отмена</button>";
    document.getElementById("edit-buttoms").innerHTML = html2;
}

function setCloseButtomDel(doctorID, dateNow, patientId) {
    var html2 = "";
    html2 += "<button type=\"button\" class=\"btn btn-outline-info\" onclick=\"SetDelMode(" + doctorID + ",\'" + dateNow + "\'," + patientId + ");\">Отмена</button>";
    document.getElementById("edit-buttoms").innerHTML = html2;
}

function setDropdowmMenu(doctorID, dateNow, patientId) {
    var html2 = "";
    html2 += "<div class=\"dropdown\">";
    html2 += "<button class=\"btn btn-outline-info dropdown-toggle\" type=\"button\" id=\"dropdownMenu2\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\">";
    html2 += "Редактирование записей</button>";
    html2 += "<div class=\"dropdown-menu\" aria-labelledby=\"dropdownMenu2\">";
    html2 += "<button class=\"dropdown-item\" type=\"button\" onclick=\"SetAddMode(" + doctorID + ",\'" + dateNow + "\'," + patientId + ")\">Добавить</button>";
    html2 += "<button class=\"dropdown-item\" type=\"button\" onclick=\"SetDelMode()\">Удалить</button>";
    html2 += "</div>";
    html2 += "</div>";
    document.getElementById("edit-buttoms").innerHTML = html2;
}

function GetPatientId() {
    return 1;
}
var addMode = false;

function SetAddMode(doctorID, dateNow, patientId) {
    addMode = true;
    var date = new Date(dateNow);
    for (let i = 0; i < 7; i++) {
        for (let j = 0; j < 4; j++) {
            if (document.getElementById("appointmen-time" + (10 + i) + "-" + (j * 15)).className == "btn btn-warning btn-block" && document.getElementById("appointmen-time" + (10 + i) + "-" + (j * 15)).disabled == true) {
                document.getElementById("appointmen-time" + (10 + i) + "-" + (j * 15)).disabled = false;
                document.getElementById("appointmen-time" + (10 + i) + "-" + (j * 15)).onclick = function () {
                    var newDate = new Date(date.getFullYear(), date.getMonth(), date.getDate(), (10 + i) + 3, (j * 15));
                    AddAppointment(doctorID, newDate.toISOString(), null);
                    document.getElementById("appointmen-time" + (10 + i) + "-" + (j * 15)).className = "btn btn-success btn-block active";
                }
            }
        }
    }
    setCloseButtomAdd(doctorID, dateNow, patientId);
}

function UNSetAddMode(doctorID, dateNow, patientId) {
    addMode = false;
    for (let i = 0; i < 7; i++) {
        for (let j = 0; j < 4; j++) {
            if (document.getElementById("appointmen-time" + (10 + i) + "-" + (j * 15)).className == "btn btn-warning btn-block" && document.getElementById("appointmen-time" + (10 + i) + "-" + (j * 15)).disabled == false) {
                document.getElementById("appointmen-time" + (10 + i) + "-" + (j * 15)).disabled = true;
            }
        }
    }
    document.getElementById("msgAdd").innerHTML = "";
    setDropdowmMenu(doctorID, dateNow, patientId);
}

function AddAppointment(doctorID, dateNow, patientId) {
    
    var nowDate = new Date(dateNow);
    let toDay = nowDate.getDay();
    if (toDay == 0) {
        toDay = 7;
    }
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.onload = function (ev) {
        var msg = ""
        if (xmlhttp.status == 401) {
            msg = "У вас не хватает прав для добавления";
        } else if (xmlhttp.status == 200) {
            msg = "Запись добавлена";
        } else {
            msg = "Неизвестная ошибка";
        }
        document.getElementById("msgAdd").innerHTML = msg;
    }
    xmlhttp.open("POST", "/api/Appointments/");
    xmlhttp.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    xmlhttp.send(JSON.stringify({
        doctorId: doctorID,
        patientId: patientId,
        daysOfWeekId: toDay,
        dataTime: nowDate.toISOString(),
        daysOfWeek: null,
        doctor: null
    }));
}
var delMod = false;

function ActionsAppointment(appointmentId, doctorId, patientId, dateNow, daysOfWeekId) {
    let html = "";
    if (delMod == false) {
        html += "<div class=\"padding-d d-flex justify-content-start\">";
        html += "<button id=\"yes-buttom\" type=\"button\" class=\"btn btn-outline-info btn-block active\">Выбрать время</button>";
        html += "</div>";
        html += "<div class=\"padding-d d-flex justify-content-between\">";
        html += "<button id=\"no-buttom\" type=\"button\" class=\"btn btn-outline-info btn-block active\">Отмена</button>";
        html += "</div>";
        document.getElementById("yes-no").innerHTML = html;
        document.getElementById("no-buttom").onclick = function () {
            document.getElementById("yes-no").innerHTML = "";
        }
        document.getElementById("yes-buttom").onclick = function () {
            AddReservAppointment(appointmentId, doctorId, patientId, dateNow, daysOfWeekId);
            document.getElementById("yes-no").innerHTML = "";
        }
    } else {
        html += "<div class=\"padding-d d-flex justify-content-start\">";
        html += "<button id=\"yes-buttom\" type=\"button\" class=\"btn btn-outline-info btn-block active\">Удалить запись</button>";
        html += "</div>";
        html += "<div class=\"padding-d d-flex justify-content-between\">";
        html += "<button id=\"no-buttom\" type=\"button\" class=\"btn btn-outline-info btn-block active\">Отмена</button>";
        html += "</div>";
        document.getElementById("yes-no").innerHTML = html;
        document.getElementById("no-buttom").onclick = function () {
            document.getElementById("yes-no").innerHTML = "";
        }
        document.getElementById("yes-buttom").onclick = function () {
            DelAppointment(appointmentId, dateNow);
            document.getElementById("yes-no").innerHTML = "";
        }
    }
}

function AddReservAppointment(appointmentId, doctorId, patientId, dateNow, daysOfWeekId) {
    xmlhttp.open("PUT", "/api/Appointments/" + appointmentId);
    xmlhttp.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    xmlhttp.send(JSON.stringify({
        appointmentId: appointmentId,
        doctorId: doctorId,
        patientId: patientId,
        daysOfWeekId: daysOfWeekId,
        dataTime: dateNow,
        daysOfWeek: null,
        doctor: null
    }));
    var dateElem = new Date(dateNow);
    document.getElementById("appointmen-time" + dateElem.getHours() + "-" + dateElem.getMinutes()).className = "btn btn-danger btn-block";
    document.getElementById("appointmen-time" + dateElem.getHours() + "-" + dateElem.getMinutes()).disabled = true;
}

function SetDelMode(doctorID, dateNow, patientId) {
    if (delMod == false) {
        delMod = true
        setCloseButtomDel();
    } else {
        delMod = false;
        setDropdowmMenu(doctorID, dateNow, patientId);
    }
}

function DelAppointment(appointmentId, dateNow) {
    var request = new XMLHttpRequest();
    url = "/api/Appointments/" + appointmentId;
    request.open("DELETE", url, false);
    request.send();
    var dateElem = new Date(dateNow);
    document.getElementById("appointmen-time" + dateElem.getHours() + "-" + dateElem.getMinutes()).className = "btn btn-warning btn-block";
    document.getElementById("appointmen-time" + dateElem.getHours() + "-" + dateElem.getMinutes()).disabled = true;
}

function Login() {
    // Считывание данных с формы
    email = document.getElementById("EmailLogin").value;
    password = document.getElementById("PasswordLogin").value;
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.open("POST", "/api/Account/Login");
    xmlhttp.setRequestHeader("Content-Type", "application/json;charset=UTF8");
    xmlhttp.onreadystatechange = function () {
        // Очистка контейнера вывода сообщений
        document.getElementById("msgLogin").innerHTML = ""
        var mydiv = document.getElementById('formErrorLogin');
        while (mydiv.firstChild) {
            mydiv.removeChild(mydiv.firstChild);
        }
        // Обработка ответа от сервера
        myObj = JSON.parse(this.responseText);
        document.getElementById("msgLogin").innerHTML = myObj.message;
        LogMenu(email);
        // Вывод сообщений об ошибках
        if (typeof myObj.error !== "undefined" && myObj.error.length > 0) {
            for (var i = 0; i < myObj.error.length; i++) {
                var ul = document.getElementsByTagName("ul");
                var li = document.createElement("li");
                li.appendChild(document.createTextNode(myObj.error[i]));
                ul[0].appendChild(li);
                13
            }
        }
        document.getElementById("Password").value = "";
    };

    xmlhttp.send(JSON.stringify({
        email: email,
        password: password
    }));
    GetCurrentUser();
};
// Обработка клика по кнопке
document.getElementById("loginBtn").addEventListener("click",
    Login);

function Register() {
    // Считывание данных с формы
    email = document.getElementById("Email").value;
    password = document.getElementById("Password").value;
    passwordConfirm = document.getElementById("PasswordConfirm").value;
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.open("POST", "/api/account/Register");
    xmlhttp.setRequestHeader("Content-Type", "application/json;charset=UTF8");
    xmlhttp.onreadystatechange = function () {
        // Очистка контейнера вывода сообщений
        document.getElementById("msg").innerHTML = ""
        var mydiv = document.getElementById('formError');
        while (mydiv.firstChild) {
            mydiv.removeChild(mydiv.firstChild);
        }
        // Обработка ответа от сервера
        myObj = JSON.parse(this.responseText);
        document.getElementById("msg").innerHTML = myObj.message;
        // Вывод сообщений об ошибках
        if (myObj.error.length > 0) {
            for (var i = 0; i < myObj.error.length; i++) {
                var ul = document.getElementsByTagName("ul");
                var li = document.createElement("li");
                li.appendChild(document.createTextNode(myObj.error[i]));
                ul[0].appendChild(li);
            }
        }
        // Очистка полей поролей
        document.getElementById("PasswordLogin").value = "";
        document.getElementById("PasswordConfirm").value = "";
    };
    // Запрос на сервер
    xmlhttp.send(JSON.stringify({
        email: email,
        password: password,
        passwordConfirm: passwordConfirm
    }));
    GetCurrentUser();
};

function LoginOff() {
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.open("POST", "api/Account/LogOff", true);
    xmlhttp.send();
    UnLogMenu();
}
// Обработка клика по кнопке
document.getElementById("registerBtn").addEventListener("click",
    Register);
var testAutenXmlHTTP = "";
var testBool = false

function GetCurrentUser() {
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.open("POST", "/api/Account/isAuthenticated", true);
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.responseText != "") {
            testAutenXmlHTTP = JSON.parse(xmlhttp.responseText);
            if (testAutenXmlHTTP.boolen == "false") {
                UnLogMenu();
            }
            if (testAutenXmlHTTP.boolen == "true") {
                LogMenu(testAutenXmlHTTP.name);
            }
        }
    }
    xmlhttp.send();
}

function UnLogMenu() {
    var html = "";
    html += "<div class=\"d-flex flex-row bd-highlight mb-3\">";
    html += "<div class=\"p-2 bd-highlight\">";
    html += "<button type=\"button\" class=\"btn btn-danger btn-block\" data-toggle=\"modal\" data-target=\"#registerModal\">Зарегистрироваться</button>";
    html += "</div>"
    html += "<div class=\"p-2 bd-highlight\">";
    html += "<button type=\"button\" class=\"btn btn-danger btn-block\" data-toggle=\"modal\" data-target=\"#loginModal\">Войти</button>"
    html += "</div>"
    html += "</div>"
    document.getElementById("login").innerHTML = html;

}

function LogMenu(name) {
    var html = "";
    html += "<div class=\"btn-group\" role=\"group\">";
    html += "<button id=\"btnGroupDrop1\" type=\"button\" class=\"btn btn-light dropdown-toggle\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\">"
    html += "<span class=\"navbar-toggler-icon\"></span>";
    html += "</button>";
    html += "<div class=\"dropdown-menu\" aria-labelledby=\"btnGroupDrop1\">";
    html += "<img src=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAJeSURBVGhD7drfa45hHMfxx49N1NRSFKUm/8B25sDIgYgIpeYApZxJlJId4IScmTWhlFIK+dVaa0hrObMxP1KM4QglP/IjI7P3tzl6+lj3fT3Xve+l+dTrYPV0P5+7nntd3+u6SxEzDRtxEb14ittoQSP+iazHa/wewxVUI9nsxjBU+XIPcRdd2IbJSCIrkfUmlEuYAtdMxQuognnshWs2QBXL6x2q4JZbUMVCrIJLZuIXVKkQx+CSZVCFQnXCJfaAqkKhBuAS+ymoQqFewSXHoQqFcruRw1CFQj2ASzZDFQrl9rDPRcx/vwfhkoV4D1UqxBG4ZD9UoVB34JLYD/sgXLILqlCofrhkEVShUEfhEpvs7OegSuX1FfPhluX4AFUujx1wzwL8gCqYhdtPSuUmVMksGpBMQpcrfUgqtgnxEqrsWNYguayDKvs3tq+VbM5DlS73HXVINluhipdrR9I5AVW83HUkG3twf0IVVw5hNtwzHfaA25HBPYTu/z7GSazGuO7UL8Y5fIYqVgkb1NpgQ1thWQEbflSB2GyEvoCoC8l5uAr1hUX7hj2YhIpix2VvoL5kPF3GDATF9naHoC7s4RpyHwjNQYw5I7Z9yJVWqAt5+4QaZE6sEbYIS5A5lRxuFm0TMkddIBUT80Zibk7HthaZY7O0uog3G9xsrM4c29mws291MQ9PsAVBsTWWLdG/QF28aB9xFra8j/K+Si2acBrPUNTz8xYdaIaNCvbKVKGxgaoe9l7WTtjxwhnYsttm8Rvogb2vdR/P8ejP392wz5zCAWzHUszC/4ymVBoBFDNEae17I/kAAAAASUVORK5CYII=\" width=\"23px\" height=\"23px\">";
    html += "<span>" + name + "</span>";
    html += "<a class=\"dropdown-item\" href=\"#\">Мой аккаунт</a>";
    html += "<a class=\"dropdown-item\" href=\"#\">Мои записи</a>";
    html += "<a id=\"login-off\"class=\"dropdown-item\" href=\"#\">Выйти</a>";
    html += "</div>"
    html += "</div>"
    document.getElementById("login").innerHTML = html;
    document.getElementById("login-off").addEventListener("click",
        LoginOff);
}