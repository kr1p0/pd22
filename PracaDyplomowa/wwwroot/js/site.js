
//Start on load
document.addEventListener("DOMContentLoaded", function () {
    //$(document).ready(function () {
    //$("#register-form").hide();
    //$("#passRecovery-form").hide();

    ActionResult("backendALert");

  

    if (document.getElementById("IsAuthenticatedCheck").value == "authenticated"    //!!!!!
        && document.getElementById("IsAdminCheck").value == "adminUser") {  

        search4Notifications();
        search4NumberOfEventsToday();

        highlightClickedTab();

        connection.start();
        connection.on("ReceiveMessage2", function (message) {
            var notificationDot = document.getElementById("navbarNotiDot");
            notificationDot.innerHTML = notificationDot.innerHTML == "" ? 1 :
                parseInt(notificationDot.innerHTML) + 1;
            notificationDot.style.backgroundColor = "#ffce32"
            notificationDot.style.fontSize = "20px";
            setTimeout(function () {
                notificationDot.style.fontSize = "13px";
            }
                , 400);
        });
    }

    spinner.style.display = "none";

    //for mobile to not auto extend sideMenu
    if ($(window).width() > 550) {
        getSideBarStatus();
    }

});


closeReadOnlyInfo();

$(".selectWithSearch").select2();


/*SignalR*/
"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/NotificationHub").build();


var spinner = document.getElementById("upperSpinner");
function waitSpinner() {
    spinner.style.display = "block";
}

$(".displaySpinner").on("submit", function (e) {
    waitSpinner()
});


// LoginRegister panel //Animations-to right
$(".moveLoginRegisterPanelR").click(function () {
    $("#login-form").fadeOut(1)
    $("#windowCover").fadeOut(1)
    $("#register-form").fadeIn(100)
    $("#windowCover").fadeIn(100)
    $("#passRecovery-form").fadeOut(0)

    el = document.getElementById("windowCover");
    el.classList.add("movePanelRight");
    setTimeout(function () {
        el.classList.add("movePanelRightPart2");
    }
        , 600);

    document.getElementById("outerFormWindow").style.height = "520px";
});

// LoginRegister panel //Animations-to left
$(".moveLoginRegisterPanelL").click(function () {
    $("#register-form").fadeOut(1)
    $("#windowCover").fadeOut(1)
    $("#login-form").fadeIn(100)
    $("#windowCover").fadeIn(100)
    $("#passRecovery-form").fadeOut(0)

    el = document.getElementById("windowCover")
    el.style.marginLeft = "-10px";
    el.classList.remove("movePanelRight");
    el.classList.remove("movePanelRightPart2");

    setTimeout(function () {
        el.style.transitionDuration = "0.25s";
        el.style.marginLeft = "0";
    }
        , 600);
    setTimeout(function () {
        el.style.transitionDuration = "";

    }
        , 800);
    document.getElementById("outerFormWindow").style.height = "400px";

});

// LoginRegister panel //Animations - fade
$(".forgotPassLoginPanel").click(function () {
    $("#login-form").fadeOut(1)
    $("#windowCover").fadeOut(1)
    $("#passRecovery-form").fadeIn(100)
    $("#windowCover").fadeIn(30)


});

// LoginRegister panel //Animations -fade
$(".backFromRecoveryPanel").click(function () {
    $("#passRecovery-form").fadeOut(1)
    $("#windowCover").fadeOut(1)
    $("#login-form").fadeIn(100)
    $("#windowCover").fadeIn(30)


});




// LoginRegister -->Mobile<-- panel 
$("#goToMobileRegisterPanel").click(function () {
    $("#MobileLoginForm").fadeOut(1)
    $("#MobileRegisterForm").fadeIn(700)
});

$("#goToPassRecoverPanel").click(function () {
    $("#MobileLoginForm").fadeOut(1)
    $("#MobilepassRecoveryForm").fadeIn(700)
});

$("#goBackToLoginMobilePanel2").click(function () {
    $("#MobileRegisterForm").fadeOut(1)
    $("#MobileLoginForm").fadeIn(700)
});

$("#goBackToLoginMobilePanel").click(function () {
    $("#MobilepassRecoveryForm").fadeOut(1)
    $("#MobileLoginForm").fadeIn(700)
});

//get scroll direction
$(document).on('mousewheel', function (e) {
    if ($(window).width() < 550) {
        if (event.wheelDelta && event.wheelDelta > 0) {
            //console.log('UP');
            $(".customNavBar").animate({ marginTop: 0 }, 10);
            $(".mainNavTabs").css("margin-top", "0px");
            $(".upperMenuBar").css("margin-top", "0px");
            var r = document.querySelector(':root');
            r.style.setProperty('--innerContainerHeight', 'calc(100vh - 160px)');
            r.style.setProperty('--innerContainer2Height', 'calc(100vh - 110px)');
            r.style.setProperty('--innerContainer3Height', 'calc(100vh - 80px)');
            r.style.setProperty('--innerContainer4Height', 'calc(100vh - 80px)');
            r.style.setProperty('--mebuBarHeight', 'calc(100vh - 45px )');
          

        }
        else if (event.wheelDelta && event.wheelDelta < 0) {
            //console.log('Down');
            $(".customNavBar").animate({ marginTop: -55 }, 10);
            $(".mainNavTabs").css("margin-top", "10px");
            $(".upperMenuBar").css("margin-top", "10px");
            var r = document.querySelector(':root');
            r.style.setProperty('--innerContainerHeight', 'calc(100vh - 110px)');
            r.style.setProperty('--innerContainer2Height', 'calc(100vh - 60px)');
            r.style.setProperty('--innerContainer3Height', 'calc(100vh - 30px)');
            r.style.setProperty('--innerContainer4Height', 'calc(100vh - 30px)');
            r.style.setProperty('--mebuBarHeight', 'calc(102vh)');

        }
    }

});

//get scroll direction
$(document).on('touchstart', function (e) {

    var swipe = e.originalEvent.touches,
        start = swipe[0].pageY;

    $(this).on('touchmove', function (e) {

        var contact = e.originalEvent.touches,
            end = contact[0].pageY,
            distance = end - start;

        if (distance > 30) {
            $(".customNavBar").animate({ marginTop: 0 }, 100);
            $(".mainNavTabs").css("margin-top", "0px");
            $(".upperMenuBar").css("margin-top", "0px");
            var r = document.querySelector(':root');
            r.style.setProperty('--innerContainerHeight', 'calc(100vh - 160px)');
            r.style.setProperty('--innerContainer2Height', 'calc(100vh - 110px)');
            r.style.setProperty('--innerContainer3Height', 'calc(100vh - 80px)');
            r.style.setProperty('--innerContainer4Height', 'calc(100vh - 80px)');
            r.style.setProperty('--mebuBarHeight', 'calc(100vh - 45px )');
        }
        //up
        if (distance < -30) {
            $(".customNavBar").animate({ marginTop: -55 }, 100);
            $(".mainNavTabs").css("margin-top", "10px");
            $(".upperMenuBar").css("margin-top", "10px");
            var r = document.querySelector(':root');
            r.style.setProperty('--innerContainerHeight', 'calc(100vh - 110px)');
            r.style.setProperty('--innerContainer2Height', 'calc(100vh - 60px)');
            r.style.setProperty('--innerContainer3Height', 'calc(100vh - 30px)');
            r.style.setProperty('--innerContainer4Height', 'calc(100vh - 30px)');
            r.style.setProperty('--mebuBarHeight', 'calc(102vh)');
        }
    })
        .one('touchend', function () {
            $(this).off('touchmove touchend');
            start = swipe[0].pageY;
        });
});



function prepareDateNowForInput() {
    var currentDate = new Date()
    var currentMonthNumber = currentDate.getMonth() + 1;
    currentMonthNumber = currentMonthNumber < 10 ? "0" + currentMonthNumber : currentMonthNumber;
    var currentYear = currentDate.getFullYear();
    var currentDay = currentDate.getDate();
    currentDay = currentDay < 10 ? "0" + currentDay : currentDay;

    return currentYear + "-" + currentMonthNumber + "-" + currentDay;
}

//Register panel // check if passwords are equal
function checkIfPasswordIsEqual(sender) {
    var pass = document.getElementById("registerPass").value;
    var confirmPass = document.getElementById("registerConfirmPass").value;
    var displayconfirmPassError = document.getElementById("displayconfirmPassError");
    var displayPassError = document.getElementById("displayPassError");
    document.getElementById("registerFormSubmit").disabled = false;
    if (pass && confirmPass) {
        displayPassError.innerText = "";
        displayconfirmPassError.innerText = "";
        if (pass != confirmPass) {
            document.getElementById(sender).innerText = "Podane hasła się nie zgadzają!";
            document.getElementById(sender).style.color = "tomato";
            document.getElementById("registerFormSubmit").disabled = true;
        }
    }
}

//Register panel Mobile // check if passwords are equal
function checkIfPasswordIsEqualMobile(sender) {
    var pass = document.getElementById("registerPassSm").value;
    var confirmPass = document.getElementById("registerConfirmPassSm").value;
    var displayconfirmPassError = document.getElementById("displayconfirmPassErrorSm");
    var displayPassError = document.getElementById("displayPassErrorSm");
    document.getElementById(sender).disabled = false;
    if (pass && confirmPass) {
        displayPassError.innerText = "";
        displayconfirmPassError.innerText = "";
        if (pass != confirmPass) {
            document.getElementById(sender).innerText = "Podane hasła się nie zgadzają!";
            document.getElementById(sender).style.color = "tomato";
            document.getElementById("registerFormSubmit").disabled = true;
        }
    }
}

//UpdatePass panel // check if passwords are equal
function checkIfUpdatePasswordIsEqual(sender) {
    var pass = document.getElementById("updateMemberPass").value;
    var confirmPass = document.getElementById("updateMemberPassConfirm").value;
    var displayconfirmPassError = document.getElementById("displayConfirmUpdatePassError");
    var displayPassError = document.getElementById("displayUpdatePassError");
    document.getElementById("submitPassUpdate").disabled = false;
    document.getElementById("alternativeSubmitPassUpdate").disabled = false;
    if (pass && confirmPass) {
        displayPassError.innerText = "";
        displayconfirmPassError.innerText = "";
        if (pass != confirmPass) {
            document.getElementById(sender).innerText = "Podane hasła się nie zgadzają!";
            document.getElementById(sender).style.color = "tomato";
            document.getElementById("submitPassUpdate").disabled = true;
            document.getElementById("alternativeSubmitPassUpdate").disabled = true;
        }
    }
}

function getCookie(name) {
    let cookieName = name + "=";
    let arr = document.cookie.split(';');
    for (let i = 0; i < arr.length; i++) {
        var c = arr[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(cookieName) == 0)
            return c.substring(cookieName.length, c.length);
    }
    return null;
}

//CREATES SLIDEDOWN ALERT 
function getAlertSlideUp(msg) {
    spinner.style.display = "none";
    var el = document.createElement("div");
    el.id = "ele";

    // BOTTOM SMALL SLIDER ON CENTER
    el.setAttribute("style", "position: fixed; ; text-align:center; bottom: 25px\
			;left: 50%;transform: translate(-50%, 0); background: rgba(2, 6, 11, 0.8)\
			;font-size:20px; color:#fff ; border-radius: 10px; padding:12px 40px ;display:none;\
			; z-index:3");
    //el.classList.add("box-shadow");

    el.innerHTML = msg;
    document.body.appendChild(el);
    setTimeout(function () {
        $("#ele").slideDown(400);
    }
        , 700);


    //aditional
    setTimeout(function () {
        $("#ele").slideUp(1200);
    }
        , 1100);

    setTimeout(function () {
        $("#ele").stop();
    }
        , 1300);

    setTimeout(function () {
        $("#ele").slideDown(400);
    }
        , 1300);
    //aditional


    setTimeout(function () {
        $("#ele").slideUp(400);
    }
        , 3500);

    setTimeout(function () {
        document.body.removeChild(el);
    }
        , 4300);
}

function ActionResult(elementId) {
    let elements = document.getElementsByClassName(elementId);
    for (let i = 0; i < elements.length; i++) {
        if (elements[i].value != "") {
            getAlertSlideUp(elements[i].value);
            elements[i].value = "";
        }
    }


}


//extends menuBar on icon click 
function extendSidebar() {
    let menuBar = document.getElementById("menuBarLeft");
    let extendSidebarBtn = document.getElementById("extendSidebarBtn");
    let extendSidebarBtnContainerSm = document.getElementById("extendSidebarBtnContainerSm");
    if (menuBar.classList.contains("menuBarTablePartlyHidden")) {
        menuBar.classList.add("menuBarTableExtended");
        extendSidebarBtnContainerSm.classList.add("menuBarSmButtonExtended");
        extendSidebarBtn.classList.add("rotate90");
        menuBar.classList.remove("menuBarTablePartlyHidden");
        extendSidebarBtnContainerSm.classList.remove("menuBarSmButtonPartlyHidden");
        var menuBarStatus = "menuBarTableExtended";
    }
    else {
        extendSidebarBtnContainerSm.classList.remove("menuBarSmButtonExtended");
        extendSidebarBtnContainerSm.classList.add("menuBarSmButtonPartlyHidden");
        menuBar.classList.add("menuBarTablePartlyHidden");
        extendSidebarBtn.classList.remove("rotate90");
        menuBar.classList.remove("menuBarTableExtended");
        menuBarStatus = "menuBarTablePartlyHidden";
    }

    document.cookie = `menuBarStatus= ${menuBarStatus} ; max-age=31536000 ;path=/`; //60*60*24*365 = 1year //;path=/ for all subpages

    //sessionStorage (works until browser gets closed)
    //localStorage.setItem("someVarKey", status);


    //Ajax post for saving cookie via backend
    /*
    $.ajax({
        type: "POST",
        url: "/Index?handler=SaveCookie",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: {
            "key": "menuBarBtnSetting",
            "value": status,
            //"expireTime": 180, //minutes
        },

        failure: function (response) {
            alert("Error@AjaxSaveCookie()")
        },
        error: function (response) {
            alert("Error@AjaxSaveCookie()")
        }
    });
    */
}

//sets the leftMenuBar on previous setting(on page load)
function getSideBarStatus() {

    let menuBar = document.getElementById("menuBarLeft");
    let menuBarExtendBtnSm = document.getElementById("extendSidebarBtnContainerSm");

    cookieX = getCookie("menuBarStatus");

    if (cookieX == "menuBarTableExtended" && menuBar) {
        menuBar.classList.add("menuBarTableExtended");
        extendSidebarBtn.classList.add("rotate90");
        menuBarExtendBtnSm.classList.add("menuBarSmButtonExtended");
        menuBar.classList.remove("menuBarTablePartlyHidden");
        menuBarExtendBtnSm.classList.remove("menuBarSmButtonPartlyHidden");

    }

    //sessionStorage (works until browser gets closed)
    /*
    var stat = localStorage.getItem("someVarKey");
    if (stat == "menuBarTableExtended") {
        menuBar.classList.add("menuBarTableExtended");
        extendSidebarBtn.classList.add("rotate90");
        menuBar.classList.remove("menuBarTablePartlyHidden");
    }
    */

    //Ajax Get cookie via backend
    /*
    $.ajax({
        url: "/Index?handler=FindCookie",
        type: "get",
        data: {
            key: "menuBarBtnSetting"
        },
        success: function (data) {
            if (data == "menuBarTableExtended") {
                menuBar.classList.add("menuBarTableExtended");
                extendSidebarBtn.classList.add("rotate90");
                menuBar.classList.remove("menuBarTablePartlyHidden");
            }
        },
        failure: function (er) {
            alert("Error@AjaxGetCookie()")
        },
        error: function (er) {
            alert("Error@AjaxGetCookie()")
        }
    });
    */
}

//Funkcja pobiera 4 czlon url(podzielony '/' Nastepnie wedlug strony aktywuje tab w bocznym doku)
function highlightClickedTab() {
    let location = window.location.href;
    let segmentArray = location.split('/');
    //let lastSegment = segmentArray.pop(); //last element of page url
    let subPage = segmentArray[3];
    if (subPage == "Members") {
        let el = document.getElementById("menuBarTableRow1")
        el.classList.add("selectedMenuItem");
        el.lastElementChild.lastElementChild.lastElementChild.src = "../img/black/people-fill.svg";
        el.firstElementChild.lastElementChild.lastElementChild.style.color = "black";
    }
    else if (subPage == "Equipment") {
        let el = document.getElementById("menuBarTableRow2")
        el.classList.add("selectedMenuItem");
        el.lastElementChild.lastElementChild.lastElementChild.src = "../img/black/extinguisher-fill.svg";
        el.firstElementChild.lastElementChild.lastElementChild.style.color = "black";
    }
    else if (subPage == "Actions") {
        let el = document.getElementById("menuBarTableRow3")
        el.classList.add("selectedMenuItem");
        el.lastElementChild.lastElementChild.lastElementChild.src = "../img/black/cone-striped.svg";
        el.firstElementChild.lastElementChild.lastElementChild.style.color = "black";
    }
    else if (subPage == "Calendar") {
        let el = document.getElementById("menuBarTableRow4")
        el.classList.add("selectedMenuItem");
        el.lastElementChild.lastElementChild.lastElementChild.src = "../img/black/calendar-week-fill.svg";
        el.firstElementChild.lastElementChild.lastElementChild.style.color = "black";
    }
    else if (subPage == "Competition") {
        let el = document.getElementById("menuBarTableRow5")
        el.classList.add("selectedMenuItem");
        el.lastElementChild.lastElementChild.lastElementChild.src = "../img/black/stopwatch-fill.svg";
        el.firstElementChild.lastElementChild.lastElementChild.style.color = "black";
    }
    else if (subPage == "Reminders") {
        let el = document.getElementById("menuBarTableRow6")
        el.classList.add("selectedMenuItem");
        el.lastElementChild.lastElementChild.lastElementChild.src = "../img/black/list-ul.svg";
        el.firstElementChild.lastElementChild.lastElementChild.style.color = "black";
    }
    else if (subPage == "Statistics") {
        let el = document.getElementById("menuBarTableRow7")
        el.classList.add("selectedMenuItem");
        el.lastElementChild.lastElementChild.lastElementChild.src = "../img/black/clipboard-data.svg";
        el.firstElementChild.lastElementChild.lastElementChild.style.color = "black";
    }
    else if (subPage == "Documents") {
        let el = document.getElementById("menuBarTableRow8")
        el.classList.add("selectedMenuItem");
        el.lastElementChild.lastElementChild.lastElementChild.src = "../img/black/file-earmark-pdf-fill.svg";
        el.firstElementChild.lastElementChild.lastElementChild.style.color = "black";
    }
    else if (subPage == "QRCode") {
        let el = document.getElementById("menuBarTableRow9")
        el.classList.add("selectedMenuItem");
        el.lastElementChild.lastElementChild.lastElementChild.src = "../img/black/qr-code-scan.svg";
        el.firstElementChild.lastElementChild.lastElementChild.style.color = "black";
    }
    else if (subPage == "Notifications") {
        let el = document.getElementById("menuBarTableRow10")
        el.classList.add("selectedMenuItem");
        el.lastElementChild.lastElementChild.lastElementChild.src = "../img/black/bell-fill.svg";
        el.firstElementChild.lastElementChild.lastElementChild.style.color = "black";
    }
    else if (subPage == "Finances") {
        let el = document.getElementById("menuBarTableRow11")
        el.classList.add("selectedMenuItem");
        el.lastElementChild.lastElementChild.lastElementChild.src = "../img/black/currency-dollar.svg";
        el.firstElementChild.lastElementChild.lastElementChild.style.color = "black";
    }
    else if (subPage == "Chat") {
        let el = document.getElementById("menuBarTableRow12")
        el.classList.add("selectedMenuItem");
        el.lastElementChild.lastElementChild.lastElementChild.src = "../img/black/chat-dots-fill.svg";
        el.firstElementChild.lastElementChild.lastElementChild.style.color = "black";
    }
    else if (subPage == "Announcement") {
        let el = document.getElementById("menuBarTableRow13")
        el.classList.add("selectedMenuItem");
        el.lastElementChild.lastElementChild.lastElementChild.src = "../img/black/broadcast.svg";
        el.firstElementChild.lastElementChild.lastElementChild.style.color = "black";
    }
    else if (subPage == "Settings") {
        let el = document.getElementById("menuBarTableRow14")
        el.classList.add("selectedMenuItem");
        el.lastElementChild.lastElementChild.lastElementChild.src = "../img/black/gear-wide-connected.svg";
        el.firstElementChild.lastElementChild.lastElementChild.style.color = "black";
    }
}

//Auto resize textarea
function auto_grow(sender) {
    if (sender.offsetHeight < sender.scrollHeight) {
        sender.style.height = (sender.scrollHeight + 2) + "px";
    }

}


function updateTableCheckBox(sender, tableId) {
    setTimeout(function () {
        var table = document.getElementById(tableId);
        var thisCheckbox = table.rows[sender.rowIndex].cells[0].lastElementChild;
        thisCheckbox.checked = thisCheckbox.checked ? false : true;

        if (thisCheckbox.checked)
            table.rows[sender.rowIndex].classList.add("selectedTableSmRow")
        else if (table.rows[sender.rowIndex].classList.contains("selectedTableSmRow"))
            table.rows[sender.rowIndex].classList.remove("selectedTableSmRow");

    }
        , 150);
}


function displayImgOnHover() {
    var yOff = 15; // Horizontal position of image relative to mousepointer.
    var xOff = +30; // Vertical position of image relative to mousepointer
    var pathToImage = "../img/black/cone-striped.svg";

    $(".text-hover-image").hover(function (e) {
        $("body").append("<p id='image-when-hovering-text'><img src='" + pathToImage + "'/></p>");
        $("#image-when-hovering-text")
            .css("position", "absolute")
            .css("top", (e.pageY - yOff) + "px")
            .css("left", (e.pageX + xOff) + "px")
            .fadeIn("fast");
    },

        function () {
            $("#image-when-hovering-text").remove();
        });

    $(".text-hover-image").mousemove(function (e) {
        $("#image-when-hovering-text")
            .css("top", (e.pageY - yOff) + "px")
            .css("left", (e.pageX + xOff) + "px");
    });
}

//##########################################################Members.MembersOverview


function onloadMember() {
    var filter = getCookie("FilterMemberOverview");
    var sort = getCookie("SortMemberOverview");

    if (filter == "management")
        document.getElementById("MemberCustomRadioFilter1").checked = true;
    else if (filter == "all")
        document.getElementById("MemberCustomRadioFilter2").checked = true;

    if (sort == "name")
        document.getElementById("MemberCustomRadioSort1").checked = true;
    else if (sort == "lastName")
        document.getElementById("MemberCustomRadioSort2").checked = true;
    else if (sort == "function")
        document.getElementById("MemberCustomRadioSort3").checked = true;
    else if (sort == "date")
        document.getElementById("MemberCustomRadioSort4").checked = true;


    $("#membersTable td").click(function () {
        //Gets row and column number of click
        var mytable = document.getElementById("membersTable");
        var columnNum = parseInt($(this).index()) + 1;
        var rowNum = parseInt($(this).parent().parent().index());
        var hiddenIdValue = mytable.rows[rowNum].cells[0].lastElementChild.value;

        var newLocation = location.toString()
        newLocation = newLocation.substring(0, newLocation.indexOf('MembersOverview'));
        newLocation += "ViewMember?selectedMemberId=" + hiddenIdValue;
        window.location.href = newLocation;
    });
}




function searchMemberOverview() {
    var searchInput, searchInputUpper, table, rowArr, column, column2, txtValue;
    searchInput = document.getElementById("searchMemberOverviewInput");
    searchInputUpper = searchInput.value.toUpperCase();
    table = document.getElementById("membersTable");
    rowArr = table.getElementsByTagName("tr");
    columnsCount = table.rows[0].cells.length

    for (let i = 0; i < rowArr.length; i++) {
        column = rowArr[i].getElementsByTagName("td")[1];
        column2 = rowArr[i].getElementsByTagName("td")[2];
        column3 = rowArr[i].getElementsByTagName("td")[3];

        if (column || column2 || column3) {
            txtValue = (column.textContent || column.innerText) +
                (column2.textContent || column2.innerText) +
                (column3.textContent || column3.innerText);


            if (txtValue.toUpperCase().indexOf(searchInputUpper) > -1) {
                rowArr[i].style.display = "";
            } else {
                rowArr[i].style.display = "none";
            }
        }
    }



}


//##########################################################Members.Courses
function addMemeberCoursRow() {
    
    var table = document.getElementById("coursesTable");
    var row = table.insertRow()

    var cell = row.insertCell();
    cell.innerHTML = ' <img src="/img/gray/x-circle-fill.svg" onclick="removeCourse()">';

    cell = row.insertCell();
    cell.innerHTML = '<textarea   rows="1"> </textarea>';

    cell = row.insertCell();
    cell.innerHTML = '<input type="date"/>';

    cell = row.insertCell();
    cell.innerHTML = '<input type="date"/>';

    var myDiv = document.getElementById("coursesCointainer");
    setTimeout(function () {
        myDiv.scrollTop = myDiv.scrollHeight;
    }
        , 200);
    if (document.getElementById("noContentCourses"))
        document.getElementById("noContentCourses").style.display = "none";

}

function onloadCourses() {
    $("#coursesTableForm").on("submit", function (e) {
        e.preventDefault()
        saveMemberCourses();
    });

}

function saveMemberCourses() {
    var selectedUserId = document.getElementById("coursesSelectedUserId").value;
    var listOfCourses = [];
    var table = document.getElementById("coursesTable");
    var lastRowIndex = table.rows.length - 1;

  
    for (let i = 1; i < lastRowIndex + 1; i++) {
        if (table.rows[i].cells[1].lastElementChild.value) {
            obj = {
                "UzytkownikId": selectedUserId,
                "Kurs": table.rows[i].cells[1].lastElementChild.value,
                "KursDataUkonczenia": table.rows[i].cells[2].lastElementChild.value,
                "KursDataWaznosci": table.rows[i].cells[3].lastElementChild.value
            }
            listOfCourses.push(obj);
        }
        else {
            getAlertSlideUp("(🛆) Uzupełnij dane");
            return;
        }
    }


    $.ajax({
        type: "POST",
        url: "/Members/courses?handler=SaveCoursesAjax",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: {
            "obj": listOfCourses,
        },
        success: function (data) {
            //getAlertSlideUp(data);
           
            var newLocation = location.toString()
            newLocation = newLocation.substring(0, newLocation.indexOf('Members'));
            newLocation += "Members/ViewMember?selectedMemberId=" + selectedUserId + "&passAlert=" + data ;
            window.location.href = newLocation;
        },

        failure: function (response) {
            getAlertSlideUp("Ajax@saveMemberCourses(): Nieudana operacja");
        },
        error: function (response) {
            getAlertSlideUp("Ajax@saveMemberCourses(): Nieudana operacja");
        },
        complete: function (response) {
            spinner.style.display = "none";
        },
    });


}

function removeCourse(sender) {
  
    $.ajax({
        type: "POST",
        url: "/Members/Courses?handler=RemoveCourseAjax",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: {
            idToRemove: sender,
        },
        success: function (data) {
            $(".innerContainerMembers").load(location.href + " .innerContainerMembers"); // reload div. Whitespace important

        },
        failure: function (response) {
            getAlertSlideUp("Ajax@SaveChatMsg(): Nieudana operacja");
        },
        error: function (response) {
            getAlertSlideUp("Ajax@SaveChatMsg(): Nieudana operacja");
        },
        complete: function (response) {
        }
    });
}


//##########################################################Members.ViewMember
$("#viewMemberActionTable td").click(function () {
    //Gets row and column number of click
    var mytable = document.getElementById("viewMemberActionTable");
    var columnNum = parseInt($(this).index()) + 1;
    var rowNum = parseInt($(this).parent().parent().index())-1;
    var hiddenIdValue = mytable.rows[rowNum].cells[0].lastElementChild.value;
    var newLocation = location.toString()
    newLocation = newLocation.substring(0, newLocation.indexOf('Members'));
    newLocation += "Actions/ViewAction?selectedActionId=" + hiddenIdValue;
    window.location.href = newLocation;
});


$("#viewMemberEquipmentTable td").click(function () {
    //Gets row and column number of click
    var mytable = document.getElementById("viewMemberEquipmentTable");
    var columnNum = parseInt($(this).index()) + 1;
    var rowNum = parseInt($(this).parent().parent().index()) - 1;
    var hiddenIdValue = mytable.rows[rowNum].cells[0].lastElementChild.value;
    var newLocation = location.toString()
    newLocation = newLocation.substring(0, newLocation.indexOf('Members'));
    newLocation += "Equipment/ViewEquipment?selectedEquipmentId=" + hiddenIdValue;
    window.location.href = newLocation;
});


//##########################################################Members.EditMember


//##########################################################Members.MembersOverview


//##########################################################Members.EditPassword





//##########################################################Reminders.RemindersOverview


function onloadReminderOverview() {
    var filter = getCookie("FilterReminderOverview");
    var sort = getCookie("SortReminderOverview");

    if (filter == "current")
        document.getElementById("customRadioFilter1").checked = true;
    else if (filter == "all")
        document.getElementById("customRadioFilter2").checked = true;

    if (sort == "title")
        document.getElementById("customRadioSort1").checked = true;
    else if (sort == "content")
        document.getElementById("customRadioSort2").checked = true;
    else if (sort == "date")
        document.getElementById("customRadioSort3").checked = true;

    $("#reminderTable td").click(function () {
        //Gets row and column number of click
        var thisTable = document.getElementById("reminderTable");
        var columnNum = parseInt($(this).index()) + 1;
        var rowNum = parseInt($(this).parent().parent().index());
        var hiddenIdValue = thisTable.rows[rowNum].cells[0].lastElementChild.value;

        var newLocation = location.toString()
        newLocation = newLocation.substring(0, newLocation.indexOf('RemindersOverview'));
        newLocation += "ViewReminder?selectedReminderId=" + hiddenIdValue;
        window.location.href = newLocation;
    });
}

// SEARCH BY COLUMN 1,2
function searchRemindersOverview() {
    var searchInput, searchInputUpper, table, rowArr, column, column2, txtValue;
    searchInput = document.getElementById("searchReminderOverviewInput");
    searchInputUpper = searchInput.value.toUpperCase();
    table = document.getElementById("reminderTable");
    rowArr = table.getElementsByTagName("tr");
    columnsCount = table.rows[0].cells.length

    for (let i = 0; i < rowArr.length; i++) {
        column = rowArr[i].getElementsByTagName("td")[1];
        column2 = rowArr[i].getElementsByTagName("td")[2];

        if (column || column2) {
            txtValue = (column.textContent || column.innerText) +
                (column2.textContent || column2.innerText);


            if (txtValue.toUpperCase().indexOf(searchInputUpper) > -1) {
                rowArr[i].style.display = "";
            } else {
                rowArr[i].style.display = "none";
            }
        }
    }
}





//##########################################################Reminders.ViewReminder

$("#viewReminderRecipients td").click(function () {
    //Gets row and column number of click
    var mytable = document.getElementById("viewReminderRecipients");
    var columnNum = parseInt($(this).index()) + 1;
    var rowNum = parseInt($(this).parent().parent().index()) - 1;
    var hiddenIdValue = mytable.rows[rowNum].cells[0].lastElementChild.value;
    var newLocation = location.toString()
    newLocation = newLocation.substring(0, newLocation.indexOf('Reminders'));
    newLocation += "Members/ViewMember?selectedMemberId=" + hiddenIdValue;
    window.location.href = newLocation;
});



//##########################################################Reminders.EditReminder
function onloadEditReminder() {
    /* Added e.preventDefault() which prevents website from redirecting (reloading page) and as event I used submit */
    $("#editReminderForm").on("submit", function (e) {
        e.preventDefault()

        updateRaminder();
        userListForReminderEditVisibility();
    });
}

function updateRaminder() {

    var listOfRecipients = [];
    var table = document.getElementById("editReminderUserTable");
    var lastRowIndex = table.rows.length - 1;

    for (let i = 0; i < lastRowIndex + 1; i++) {
        if (table.rows[i].cells[0].lastElementChild.checked == true)
            listOfRecipients.push(table.rows[i].cells[1].lastElementChild.value)
    }

    if ((document.getElementById("powMailCheck").checked ||
        document.getElementById("powTelefonCheck").checked) &&
        listOfRecipients.length == 0) {
        getAlertSlideUp("(🛆) Nie wybrano adresatów");
        return;
    }


    var timeInput = document.getElementById("EPrzypDataRozpoczecia").value
        + " " + document.getElementById("EPrzypCzasRozpoczecia").value;

    var updateReminder = {
        Id: document.getElementById("reminderToEditId").value,
        Tytul: document.getElementById("EPrzypomnienieTytul").value,
        Tresc: document.getElementById("EPrzypomnienieTresc").value,
        DataRozpoczecia: document.getElementById("EPrzypDataRozpoczecia").value,
        CzasRozpoczecia: timeInput,
        PowiadomienieEmail: document.getElementById("powMailCheck").checked ? "t" : "n",
        PowiadomienieTelefon: document.getElementById("powTelefonCheck").checked ? "t" : "n",

    };

    $.ajax({
        type: "POST",
        url: "/Reminders/EditReminder?handler=UpdateReminderAjax",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: {
            "obj": updateReminder,
            "listOfRecipients": listOfRecipients,
        },
        success: function (data) {
            //getAlertSlideUp(data);


            var newLocation = location.toString()
            newLocation = newLocation.substring(0, newLocation.indexOf('EditReminder'));
            newLocation += "RemindersOverview?passAlert=" + data;
            window.location.href = newLocation;

        },

        failure: function (response) {
            getAlertSlideUp("Ajax@addNewAction(): Nieudana operacja");
        },
        error: function (response) {
            getAlertSlideUp("Ajax@addNewAction(): Nieudana operacja");
        }
    });
}

function userListForReminderEditVisibility() {
    if (document.getElementById("powTelefonCheck").checked ||
        document.getElementById("powMailCheck").checked) {
        $("#hiddenReminderListOfUser").fadeIn(700)
    }
    else {
        $("#hiddenReminderListOfUser").fadeOut(350)
    }
}







//##########################################################Notifications.NotificationsOverview


function onloadNotification() {

    var filter = getCookie("FilterNotiOverview");
    var sort = getCookie("SortNotiOverview");

    if (filter == "status")
        document.getElementById("NotiCustomRadioFilter1").checked = true;
    else if (filter == "confirmation")
        document.getElementById("NotiCustomRadioFilter2").checked = true;
    else if (filter == "all")
        document.getElementById("NotiCustomRadioFilter3").checked = true;


    if (sort == "name")
        document.getElementById("NotiCustomRadioSort1").checked = true;
    else if (sort == "lastName")
        document.getElementById("NotiCustomRadioSort2").checked = true;
    else if (sort == "email")
        document.getElementById("NotiCustomRadioSort3").checked = true;
    else if (sort == "date")
        document.getElementById("NotiCustomRadioSort4").checked = true;


}

function search4Notifications() {
    /* Ajax on Get */
    var notificationDot = document.getElementById("navbarNotiDot");
    var options = {};
    options.url = "/Notifications/NotificationsOverview?handler=Look4NotificationsAjax";
    options.type = "GET";
    options.dataType = "json";
    options.success = function (data) {
        if (data == null) return;
        if (data != 0) {
            notificationDot.innerHTML = data;
            notificationDot.style.backgroundColor = "#ffce32"
            notificationDot.style.fontSize = "17px";

            setTimeout(function () {
                notificationDot.style.fontSize = "13px";

            }
                , 400);
        }
    };
    options.error = function () {
        console.log("search4UsersToApprove(): Error while making Ajax call!");
    };
    $.ajax(options);

}

function search4NumberOfEventsToday() {
    /* Ajax on Get */
    var notificationDot = document.getElementById("navbarCalendarDot");
    var options = {};
    options.url = "/Calendar/CalendarView?handler=NumberOfEventsToday";
    options.type = "GET";
    options.dataType = "json";
    options.success = function (data) {
        if (data == null) return;
        if (data!= "0") {
            notificationDot.innerHTML = data;
            notificationDot.style.backgroundColor = "#000"
            notificationDot.style.fontSize = "17px";

            setTimeout(function () {
                notificationDot.style.fontSize = "13px";
            }
                , 400);
        }
    };
    options.error = function () {
        console.log("search4NumberOfEventsToday(): Error while making Ajax call!");
    };
    $.ajax(options);

}

// SEARCH BY COLUMN 1,2,3,4
function searchNotifiOverview() {
    var searchInput, searchInputUpper, table, rowArr, column, column2, txtValue;
    searchInput = document.getElementById("searchNotificationOverviewInput");
    searchInputUpper = searchInput.value.toUpperCase();
    table = document.getElementById("notificationTable");
    rowArr = table.getElementsByTagName("tr");
    columnsCount = table.rows[0].cells.length

    for (let i = 0; i < rowArr.length; i++) {
        column = rowArr[i].getElementsByTagName("td")[1];
        column2 = rowArr[i].getElementsByTagName("td")[2];
        column3 = rowArr[i].getElementsByTagName("td")[3];
        column4 = rowArr[i].getElementsByTagName("td")[4];

        if (column || column2 || column3 || column4) {
            txtValue = (column.textContent || column.innerText) +
                (column2.textContent || column2.innerText) +
                (column3.textContent || column3.innerText) +
                (column4.textContent || column4.innerText);

            if (txtValue.toUpperCase().indexOf(searchInputUpper) > -1) {
                rowArr[i].style.display = "";
            } else {
                rowArr[i].style.display = "none";
            }
        }
    }
}




//##########################################################Action.ActionOverview
function onloadAction() {

    var filter = getCookie("FilterActionOverview");
    var sort = getCookie("SortActionOverview");

    if (filter == "pozar")
        document.getElementById("ActionCustomRadioFilter1").checked = true;
    else if (filter == "zdarzenieDrogowe")
        document.getElementById("ActionCustomRadioFilter2").checked = true;
    else if (filter == "miejsceZagrozenia")
        document.getElementById("ActionCustomRadioFilter3").checked = true;
    else if (filter == "falszywyAlarm")
        document.getElementById("ActionCustomRadioFilter4").checked = true;
    else if (filter == "all")
        document.getElementById("ActionCustomRadioFilter5").checked = true;



    if (sort == "typZdarzenia")
        document.getElementById("ActionCustomRadioSort1").checked = true;
    else if (sort == "czasZdarzenia")
        document.getElementById("ActionCustomRadioSort2").checked = true;


}

//Gets row and column number of click
$("#ActionTable td").click(function () {

    var thisTable = document.getElementById("ActionTable");
    var columnNum = parseInt($(this).index()) + 1;
    var rowNum = parseInt($(this).parent().parent().index());
    var hiddenIdValue = thisTable.rows[rowNum].cells[0].lastElementChild.value;

    var newLocation = location.toString()
    newLocation = newLocation.substring(0, newLocation.indexOf('ActionOverview'));
    newLocation += "ViewAction?selectedActionId=" + hiddenIdValue;
    window.location.href = newLocation;

    //var newLocation = location.toString().replace("RemindersOverview", "ViewReminder?selectedReminderId=" + hiddenIdValue);
    //window.location.href = newLocation;

});


// SEARCH BY COLUMN 1,2
function searchActionOverview() {
    var searchInput, searchInputUpper, table, rowArr, column, column2, txtValue;
    searchInput = document.getElementById("searchActionOverviewInput");
    searchInputUpper = searchInput.value.toUpperCase();
    table = document.getElementById("ActionTable");
    rowArr = table.getElementsByTagName("tr");
    columnsCount = table.rows[0].cells.length

    for (let i = 0; i < rowArr.length; i++) {
        column = rowArr[i].getElementsByTagName("td")[1];
        column2 = rowArr[i].getElementsByTagName("td")[2];


        if (column || column2) {
            txtValue = (column.textContent || column.innerText) +
                (column2.textContent || column2.innerText)

            if (txtValue.toUpperCase().indexOf(searchInputUpper) > -1) {
                rowArr[i].style.display = "";
            } else {
                rowArr[i].style.display = "none";
            }
        }
    }
}

$("#assignActionsToMemberTable td").click(function () {
    var thisTable = document.getElementById("assignActionsToMemberTable");
    var columnNum = parseInt($(this).index()) + 1;
    var rowNum = parseInt($(this).parent().parent().index());

    var thisCheckbox = thisTable.rows[rowNum].cells[0].lastElementChild;

    setTimeout(function () {
        thisCheckbox.checked = thisCheckbox.checked ? false : true;
    }
        , 150);
});


function assignActionToMemberTableSave() {
    spinner.style.display = "block";
    var listOfActions = [];
    var table = document.getElementById("assignActionsToMemberTable");
    var lastRowIndex = table.rows.length - 1;

    for (let i = 1; i < lastRowIndex + 1; i++) {
        if (table.rows[i].cells[0].lastElementChild.checked == true) {
            listOfActions.push(table.rows[i].cells[1].lastElementChild.value)
        }
    }

    alert(document.getElementById("ActionOverviewSelectedUserId").value);

    $.ajax({
        type: "POST",
        url: "/Actions/ActionOverview?handler=AssignActionsAjax",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: {
            "listOfActionsId": listOfActions,
            "UzytkownikId": document.getElementById("ActionOverviewSelectedUserId").value
        },
        success: function (data) {
            //getAlertSlideUp(data);
            getAlertSlideUp("(✓) Powodzenie");
            setTimeout(function () {
                var newLocation = location.toString()
                newLocation = newLocation.substring(0, newLocation.indexOf('Actions'));
                newLocation += "Members/ViewMember?selectedMemberId=" + document.getElementById("ActionOverviewSelectedUserId").value;
                window.location.href = newLocation;
            }
                , 1200);

        },

        failure: function (response) {
            getAlertSlideUp("Ajax@editAction(): Nieudana operacja");
        },
        error: function (response) {
            getAlertSlideUp("Ajax@editAction(): Nieudana operacja");
        },
        complete: function (response) {
            spinner.style.display = "none";
        },
    });
}



//##########################################################Action.AddAction
function onloadAddAction() {
    prepareDateNowForInput()
    /* Added e.preventDefault() which prevents website from redirecting (reloading page) and as event I used submit */
    $("#addActionForm").on("submit", function (e) {
        e.preventDefault()

        addNewAction();
    });



}



function addMemberActionTableOnClick(sender) {
    var countCheckedFields = 0;
    var table = document.getElementById("addActionUserTable");
    var thisCheckbox = table.rows[sender.rowIndex].cells[0].lastElementChild;

    setTimeout(function () {
        thisCheckbox.checked = thisCheckbox.checked ? false : true;
        if (thisCheckbox.checked)
            table.rows[sender.rowIndex].classList.add("selectedTableSmRow")
        else if (table.rows[sender.rowIndex].classList.contains("selectedTableSmRow"))
            table.rows[sender.rowIndex].classList.remove("selectedTableSmRow");
      
        var lastRowIndex = table.rows.length - 1;

        for (let i = 0; i < lastRowIndex + 1; i++) {
            if (table.rows[i].cells[0].lastElementChild.checked == true)
                countCheckedFields++;
        }
        document.getElementById("DodajAkcjeLiczebnosc").value = countCheckedFields;
    }
        , 150);
}

function addNewAction() {

    var listOfParticipants = [];
    var table = document.getElementById("addActionUserTable");
    var lastRowIndex = table.rows.length - 1;
    var lastCellIndex = table.rows[lastRowIndex].cells.length - 1;
    for (let i = 0; i < lastRowIndex + 1; i++) {
        if (table.rows[i].cells[0].lastElementChild.checked == true)
            listOfParticipants.push(table.rows[i].cells[1].lastElementChild.value)
    }

    if (listOfParticipants.length == 0) {
        getAlertSlideUp("(🛆) Nie wybrano uczestników");
        return;
    }

    var rozpoczecie = document.getElementById("DodajAkcjeDataZdarzenia").value
        + " " + document.getElementById("DodajAkcjeCzasZdarzenia").value;
    var zakonczenie = document.getElementById("DodajAkcjeDataZakonczenia").value
        + " " + document.getElementById("DodajAkcjeCzasZakonczenia").value;

    var rozpoczecieDate = new Date(rozpoczecie);
    var zakonczenieDate = new Date(zakonczenie);
    if (rozpoczecieDate > zakonczenieDate) {
        getAlertSlideUp("(🛆) Czas rozpoczęcia - Czas zakończenia");
        return;
    }

    var newAction = {
        //JednostkaId: ,
        TypZdarzenia: document.getElementById("DodajAkcjeTypZdarzenia").value,
        CzasZdarzenia: rozpoczecie,
        CzasZakonczenia: zakonczenie,
        IloscUczestnikow: document.getElementById("DodajAkcjeLiczebnosc").value,
        Lokalizacja: document.getElementById("DodajAkcjeLokalizacja").value
    };


    $.ajax({
        type: "POST",
        url: "/Actions/AddAction?handler=AddActionAjax",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: {
            "obj": newAction,
            "listOfParticipants": listOfParticipants,
        },
        success: function (data) {
            //getAlertSlideUp(data);

            var newLocation = location.toString()
            newLocation = newLocation.substring(0, newLocation.indexOf('AddAction'));
            newLocation += "ActionOverview?passAlert=" + data;
            window.location.href = newLocation;

        },

        failure: function (response) {
            getAlertSlideUp("Ajax@addNewAction(): Nieudana operacja");
        },
        error: function (response) {
            getAlertSlideUp("Ajax@addNewAction(): Nieudana operacja");
        }
    });


}




//##########################################################Action.ViewAction
function onloadViewAction() {


}

$("#viewActionMemberTable td").click(function () {
    //Gets row and column number of click
    var mytable = document.getElementById("viewActionMemberTable");
    var columnNum = parseInt($(this).index()) + 1;
    var rowNum = parseInt($(this).parent().parent().index()) - 1;
    var hiddenIdValue = mytable.rows[rowNum].cells[0].lastElementChild.value;
    var newLocation = location.toString()
    newLocation = newLocation.substring(0, newLocation.indexOf('Actions'));
    newLocation += "Members/ViewMember?selectedMemberId=" + hiddenIdValue;
    window.location.href = newLocation;
});




//##########################################################Action.EditAction
function onloadEditAction() {
    $("#editActionForm").on("submit", function (e) {
        e.preventDefault()

        editAction();
    });



}

function addMemberActionEditOnClick(sender) {
    var countCheckedFields = 0;
    var table = document.getElementById("editActionUserTable");
    var thisCheckbox = table.rows[sender.rowIndex].cells[0].lastElementChild;

    setTimeout(function () {
        thisCheckbox.checked = thisCheckbox.checked ? false : true;

        if (thisCheckbox.checked)
            table.rows[sender.rowIndex].classList.add("selectedTableSmRow")
        else if (table.rows[sender.rowIndex].classList.contains("selectedTableSmRow"))
            table.rows[sender.rowIndex].classList.remove("selectedTableSmRow");

        var lastRowIndex = table.rows.length - 1;

        for (let i = 0; i < lastRowIndex + 1; i++) {
            if (table.rows[i].cells[0].lastElementChild.checked == true)
                countCheckedFields++;
        }
        document.getElementById("EdytujAkcjeLiczebnosc").value = countCheckedFields;
    }
        , 150);
}

function editAction() {

    spinner.style.display = "block";
    var listOfParticipants = [];
    var table = document.getElementById("editActionUserTable");
    var lastRowIndex = table.rows.length - 1;
    var lastCellIndex = table.rows[lastRowIndex].cells.length - 1;
    for (let i = 0; i < lastRowIndex + 1; i++) {
        if (table.rows[i].cells[0].lastElementChild.checked == true)
            listOfParticipants.push(table.rows[i].cells[1].lastElementChild.value)
    }

    if (listOfParticipants.length == 0) {
        getAlertSlideUp("(🛆) Nie wybrano uczestników");
        return;
    }

    var rozpoczecie = document.getElementById("EdytujAkcjeDataZdarzenia").value
        + " " + document.getElementById("EdytujAkcjeCzasZdarzenia").value;
    var zakonczenie = document.getElementById("EdytujAkcjeDataZakonczenia").value
        + " " + document.getElementById("EdytujAkcjeCzasZakonczenia").value;

    var rozpoczecieDate = new Date(rozpoczecie);
    var zakonczenieDate = new Date(zakonczenie);
    if (rozpoczecieDate > zakonczenieDate) {
        getAlertSlideUp("(🛆) Czas rozpoczęcia - Czas zakończenia");
        return;
    }

    var editAction = {
        Id: document.getElementById("selectedActionIdForEdit").value,
        TypZdarzenia: document.getElementById("EdytujAkcjeTypZdarzenia").value,
        CzasZdarzenia: rozpoczecie,
        CzasZakonczenia: zakonczenie,
        IloscUczestnikow: document.getElementById("EdytujAkcjeLiczebnosc").value,
        Lokalizacja: document.getElementById("EdytujAkcjeLokalizacja").value
    };


    $.ajax({
        type: "POST",
        url: "/Actions/EditAction?handler=EditActionAjax",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: {
            "obj": editAction,
            "listOfParticipants": listOfParticipants,
        },
        success: function (data) {
            //getAlertSlideUp(data);

            var newLocation = location.toString()
            newLocation = newLocation.substring(0, newLocation.indexOf('EditAction'));
            newLocation += "ActionOverview?passAlert=" + data;
            window.location.href = newLocation;

        },

        failure: function (response) {
            getAlertSlideUp("Ajax@editAction(): Nieudana operacja");
        },
        error: function (response) {
            getAlertSlideUp("Ajax@editAction(): Nieudana operacja");
        },
        complete: function (response) {
            spinner.style.display = "none";
        },
    });


}





//##########################################################Competition.CompetitionOverview
function onloadCompetition() {

    var filter = getCookie("FilterCompetitionOverview");
    var sort = getCookie("SortCompetitionOverview");

    if (filter == "miejsce1")
        document.getElementById("CompetitionCustomRadioFilter1").checked = true;
    else if (filter == "miejsce2")
        document.getElementById("CompetitionCustomRadioFilter2").checked = true;
    else if (filter == "miejsce3")
        document.getElementById("CompetitionCustomRadioFilter3").checked = true;
    else if (filter == "all")
        document.getElementById("CompetitionCustomRadioFilter4").checked = true;


    if (sort == "ZajeteMiejsce")
        document.getElementById("CompetitionCustomRadioSort1").checked = true;
    else if (sort == "Data")
        document.getElementById("CompetitionCustomRadioSort2").checked = true;


}


// SEARCH BY COLUMN 1,4
function searchFinancesOverview() {
    var searchInput, searchInputUpper, table, rowArr, column, column2, txtValue;
    searchInput = document.getElementById("searchFinancesOverviewInput");
    searchInputUpper = searchInput.value.toUpperCase();
    table = document.getElementById("financesTable");
    rowArr = table.getElementsByTagName("tr");
    columnsCount = table.rows[0].cells.length

    for (let i = 0; i < rowArr.length; i++) {
        column = rowArr[i].getElementsByTagName("td")[1];
        column2 = rowArr[i].getElementsByTagName("td")[4];


        if (column || column2) {
            txtValue = (column.textContent || column.innerText) +
                (column2.textContent || column2.innerText)

            if (txtValue.toUpperCase().indexOf(searchInputUpper) > -1) {
                rowArr[i].style.display = "";
            } else {
                rowArr[i].style.display = "none";
            }
        }
    }
}



//##########################################################Competition.AddCompetition
function onloadAddCompetition() {
    $("#addCompetitionForm").on("submit", function (e) {
        e.preventDefault()

        AddCompetition();
    });

}



function AddCompetition() {
    spinner.style.display = "block";
    var listOfParticipants = [];
    var table = document.getElementById("addCompetitionUserTable");
    var lastRowIndex = table.rows.length - 1;
    var lastCellIndex = table.rows[lastRowIndex].cells.length - 1;
    for (let i = 0; i < lastRowIndex + 1; i++) {
        if (table.rows[i].cells[0].lastElementChild.checked == true) {
            var Participants = {
                UczestnikId: table.rows[i].cells[1].lastElementChild.value,
                Rola: table.rows[i].cells[3].lastElementChild.lastElementChild.value
            }
            listOfParticipants.push(Participants)
        }


    }

    if (listOfParticipants.length == 0) {
        getAlertSlideUp("(🛆) Nie wybrano uczestników");
        return;
    }



    var addCompetition = {
        //JednostkaId:
        Szczebel: document.getElementById("DodajZawodySzczebel").value,
        Kategoria: document.getElementById("DodajZawodyKategoria").value,
        IloscUczestnikow: document.getElementById("DodajZawodyIloscUczestnikow").value,
        ZajeteMiejsce: document.getElementById("DodajZawodyMiejsce").value,
        Data: document.getElementById("DodajZawodyData").value
    };


    $.ajax({
        type: "POST",
        url: "/Competition/AddCompetition?handler=AddCompetitionAjax",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: {
            "obj": addCompetition,
            "listOfParticipants": listOfParticipants,
        },
        success: function (data) {
            //getAlertSlideUp(data);

            var newLocation = location.toString()
            newLocation = newLocation.substring(0, newLocation.indexOf('EditAction'));
            newLocation += "CompetitionOverview?passAlert=" + data;
            window.location.href = newLocation;

        },

        failure: function (response) {
            getAlertSlideUp("Ajax@editAction(): Nieudana operacja");
        },
        error: function (response) {
            getAlertSlideUp("Ajax@editAction(): Nieudana operacja");
        },
        complete: function (response) {
            spinner.style.display = "none";
        },
    });

}


function addMemberCompetitionTableOnClick(sender) {
    var countCheckedFields = 0;
    var table = document.getElementById("addCompetitionUserTable");
    var thisCheckbox = table.rows[sender.rowIndex].cells[0].lastElementChild;
    var thisHiddenRoleInput = table.rows[sender.rowIndex].cells[3].lastElementChild.lastElementChild;

    //Jesli focus jest na role input, nie chowaj elementu
    if (thisHiddenRoleInput === document.activeElement) {
        return;
    }

    setTimeout(function () {
        thisCheckbox.checked = thisCheckbox.checked ? false : true;

        if (thisCheckbox.checked)
            table.rows[sender.rowIndex].classList.add("selectedTableSmRow")
        else if (table.rows[sender.rowIndex].classList.contains("selectedTableSmRow"))
            table.rows[sender.rowIndex].classList.remove("selectedTableSmRow");

        thisCheckbox.checked ? thisHiddenRoleInput.style.display = "block" : thisHiddenRoleInput.style.display = "none";

        var lastRowIndex = table.rows.length - 1;

        for (let i = 0; i < lastRowIndex + 1; i++) {
            if (table.rows[i].cells[0].lastElementChild.checked == true)
                countCheckedFields++;
        }
        document.getElementById("DodajZawodyIloscUczestnikow").value = countCheckedFields;
    }
        , 150);
}








//##########################################################Competition.ViewCompetition
$("#viewCompetitionMember td").click(function () {
    //Gets row and column number of click
    var mytable = document.getElementById("viewCompetitionMember");
    var columnNum = parseInt($(this).index()) + 1;
    var rowNum = parseInt($(this).parent().parent().index()) - 1;
    var hiddenIdValue = mytable.rows[rowNum].cells[0].lastElementChild.value;
    var newLocation = location.toString()
    newLocation = newLocation.substring(0, newLocation.indexOf('Competition'));
    newLocation += "Members/ViewMember?selectedMemberId=" + hiddenIdValue;
    window.location.href = newLocation;
});



//##########################################################Competition.EditCompetition
function onloadEditCompetition() {
    $("#editCompetitionForm").on("submit", function (e) {
        e.preventDefault()

        EditCompetition();
    });

}

function EditCompetition() {
    spinner.style.display = "block";
    var listOfParticipants = [];
    var table = document.getElementById("editCompetitionUserTable");
    var lastRowIndex = table.rows.length - 1;
    var lastCellIndex = table.rows[lastRowIndex].cells.length - 1;
    for (let i = 0; i < lastRowIndex + 1; i++) {
        if (table.rows[i].cells[0].lastElementChild.checked == true) {
            var Participants = {
                UczestnikId: table.rows[i].cells[1].lastElementChild.value,
                Rola: table.rows[i].cells[3].lastElementChild.lastElementChild.value
            }
            listOfParticipants.push(Participants)
        }
    }

    if (listOfParticipants.length == 0) {
        getAlertSlideUp("(🛆) Nie wybrano uczestników");
        return;
    }



    var addCompetition = {
        Id: document.getElementById("selectedCompetitionIdForEdit").value,
        Szczebel: document.getElementById("EdytujZawodySzczebel").value,
        Kategoria: document.getElementById("EdytujZawodyKategoria").value,
        IloscUczestnikow: document.getElementById("EdytujZawodyIloscUczestnikow").value,
        ZajeteMiejsce: document.getElementById("EdytujZawodyMiejsce").value,
        Data: document.getElementById("EdytujZawodyData").value
    };


    $.ajax({
        type: "POST",
        url: "/Competition/EditCompetition?handler=EditCompetitionAjax",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: {
            "obj": addCompetition,
            "listOfParticipants": listOfParticipants,
        },
        success: function (data) {
            //getAlertSlideUp(data);

            var newLocation = location.toString()
            newLocation = newLocation.substring(0, newLocation.indexOf('EditAction'));
            newLocation += "CompetitionOverview?passAlert=" + data;
            window.location.href = newLocation;

        },

        failure: function (response) {
            getAlertSlideUp("Ajax@editAction(): Nieudana operacja");
        },
        error: function (response) {
            getAlertSlideUp("Ajax@editAction(): Nieudana operacja");
        },
        complete: function (response) {
            spinner.style.display = "none";
        },
    });

}

function editMemberCompetitionTableOnClick(sender) {
    var countCheckedFields = 0;
    var table = document.getElementById("editCompetitionUserTable");
    var thisCheckbox = table.rows[sender.rowIndex].cells[0].lastElementChild;
    var thisHiddenRoleInput = table.rows[sender.rowIndex].cells[3].lastElementChild.lastElementChild;

    //Jesli focus jest na role input, nie chowaj elementu
    if (thisHiddenRoleInput === document.activeElement) {
        return;
    }

    setTimeout(function () {
        thisCheckbox.checked = thisCheckbox.checked ? false : true;

        if (thisCheckbox.checked)
            table.rows[sender.rowIndex].classList.add("selectedTableSmRow")
        else if (table.rows[sender.rowIndex].classList.contains("selectedTableSmRow"))
            table.rows[sender.rowIndex].classList.remove("selectedTableSmRow");

        thisCheckbox.checked ? thisHiddenRoleInput.style.display = "block" : thisHiddenRoleInput.style.display = "none";

        var lastRowIndex = table.rows.length - 1;

        for (let i = 0; i < lastRowIndex + 1; i++) {
            if (table.rows[i].cells[0].lastElementChild.checked == true)
                countCheckedFields++;
        }
        alert(countCheckedFields);
        document.getElementById("EdytujZawodyIloscUczestnikow").value = countCheckedFields;
    }
        , 150);
}




//##########################################################Equipment.EquipmentOverview
function onloadEquipment() {

    var filter = getCookie("FilterEquipmentOverview");
    var sort = getCookie("SortEquipmentOverview");

    if (filter == "cars")
        document.getElementById("EquipmentCustomRadioFilter1").checked = true;
    else if (filter == "tools")
        document.getElementById("EquipmentCustomRadioFilter2").checked = true;
    else if (filter == "uniforms")
        document.getElementById("EquipmentCustomRadioFilter3").checked = true;
    else if (filter == "inspection")
        document.getElementById("EquipmentCustomRadioFilter4").checked = true;
    else if (filter == "all")
        document.getElementById("EquipmentCustomRadioFilter5").checked = true;


    if (sort == "manufacturer")
        document.getElementById("EquipmentCustomRadioSort1").checked = true;
    else if (sort == "model")
        document.getElementById("EquipmentCustomRadioSort2").checked = true;
    else if (sort == "date")
        document.getElementById("EquipmentCustomRadioSort3").checked = true;


}


// SEARCH BY COLUMN 1,2
function searchEquipmentOverview() {
    var searchInput, searchInputUpper, table, rowArr, column, column2, txtValue;
    searchInput = document.getElementById("searchEquipmentOverviewInput");
    searchInputUpper = searchInput.value.toUpperCase();
    table = document.getElementById("equipmentTable");
    rowArr = table.getElementsByTagName("tr");
    columnsCount = table.rows[0].cells.length

    for (let i = 0; i < rowArr.length; i++) {
        column = rowArr[i].getElementsByTagName("td")[1];
        column2 = rowArr[i].getElementsByTagName("td")[2];


        if (column || column2) {
            txtValue = (column.textContent || column.innerText) +
                (column2.textContent || column2.innerText)

            if (txtValue.toUpperCase().indexOf(searchInputUpper) > -1) {
                rowArr[i].style.display = "";
            } else {
                rowArr[i].style.display = "none";
            }
        }
    }
}

//Gets row and column number of click
$("#equipmentTable td").click(function () {
    var thisTable = document.getElementById("equipmentTable");
    var columnNum = parseInt($(this).index()) + 1;
    var rowNum = parseInt($(this).parent().parent().index());
    var hiddenIdValue = thisTable.rows[rowNum].cells[0].lastElementChild.value;

    var newLocation = location.toString()
    newLocation = newLocation.substring(0, newLocation.indexOf('ActionOverview'));
    newLocation += "ViewEquipment?selectedEquipmentId=" + hiddenIdValue;
    window.location.href = newLocation;

    //var newLocation = location.toString().replace("RemindersOverview", "ViewReminder?selectedReminderId=" + hiddenIdValue);
    //window.location.href = newLocation;

});



$("#assignEquipmentToMemberTable td").click(function () {
    var thisTable = document.getElementById("assignEquipmentToMemberTable");
    var columnNum = parseInt($(this).index()) + 1;
    var rowNum = parseInt($(this).parent().parent().index());
    if (columnNum == 5) {
        return;
    }
    var thisCheckbox = thisTable.rows[rowNum].cells[0].lastElementChild;

    setTimeout(function () {
        thisCheckbox.checked = thisCheckbox.checked ? false : true;
    }
        , 150);
});


function assignEquipmentToMemberTableSave() {
    spinner.style.display = "block";
    var listOfEquipment = [];
    var table = document.getElementById("assignEquipmentToMemberTable");
    var lastRowIndex = table.rows.length - 1;

    for (let i = 1; i < lastRowIndex + 1; i++) {
        if (table.rows[i].cells[0].lastElementChild.checked == true) {
            var assignedEquipment = {
                Id: table.rows[i].cells[1].lastElementChild.value,
                Ilosc: table.rows[i].cells[4].lastElementChild.value
            }
            listOfEquipment.push(assignedEquipment)
        }
    }

    if (listOfEquipment.length == 0) {
        getAlertSlideUp("(🛆) Nie przypisano sprzętu");
        return;
    }



    $.ajax({
        type: "POST",
        url: "/Equipment/EquipmentOverview?handler=AssignEquipmentAjax",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: {
            "listOfEquipment": listOfEquipment,
            "UzytkownikId": document.getElementById("equipmentOverviewSelectedUserId").value
        },
        success: function (data) {
            //getAlertSlideUp(data);
            getAlertSlideUp("(✓) Powodzenie");
            setTimeout(function () {
                var newLocation = location.toString()
                newLocation = newLocation.substring(0, newLocation.indexOf('Equipment'));
                newLocation += "Members/ViewMember?selectedMemberId=" + document.getElementById("equipmentOverviewSelectedUserId").value;
                window.location.href = newLocation;
            }
                , 1200);
           
        },

        failure: function (response) {
            getAlertSlideUp("Ajax@editAction(): Nieudana operacja");
        },
        error: function (response) {
            getAlertSlideUp("Ajax@editAction(): Nieudana operacja");
        },
        complete: function (response) {
            spinner.style.display = "none";
        },
    });
}



//##########################################################Equipment.EquipmentOverview
$("#viewMembersAssignedToEquipmentTable td").click(function () {
    //Gets row and column number of click
    var mytable = document.getElementById("viewMembersAssignedToEquipmentTable");
    var columnNum = parseInt($(this).index()) + 1;
    var rowNum = parseInt($(this).parent().parent().index()) - 1;
    var hiddenIdValue = mytable.rows[rowNum].cells[0].lastElementChild.value;
    var newLocation = location.toString()
    newLocation = newLocation.substring(0, newLocation.indexOf('Equipment'));
    newLocation += "Members/ViewMember?selectedMemberId=" + hiddenIdValue;
    window.location.href = newLocation;
});








//##########################################################Statistics.StatisticsOverview
var arrForChart = [];
var minDateForChart = "";

function onloadStatistics() {
    var selectedChart = getCookie("StatisticsRadioNum")

    if (selectedChart == 1)
        document.getElementById("StatisticsCustomRadio1").checked = true;
    else if (selectedChart == 2)
        document.getElementById("StatisticsCustomRadio2").checked = true;
    else if (selectedChart == 2.5)
        document.getElementById("StatisticsCustomRadio2.5").checked = true;
    else if (selectedChart == 3)
        document.getElementById("StatisticsCustomRadio3").checked = true;
    else if (selectedChart == 4)
        document.getElementById("StatisticsCustomRadio4").checked = true;
    else if (selectedChart == 5)
        document.getElementById("StatisticsCustomRadio5").checked = true;
    else if (selectedChart == 5.5)
        document.getElementById("StatisticsCustomRadio5.5").checked = true;
    else if (selectedChart == 6)
        document.getElementById("StatisticsCustomRadio6").checked = true;
    else if (selectedChart == 7)
        document.getElementById("StatisticsCustomRadio7").checked = true;
    selectedStatisticsToDisplay();

    autoResizeChart();
}



//zajete miejsca w zawodach oraz ilosc wystepowania danego miejsca
function getChartCompetitionPlace_Count(minDate, maxDate) {
    spinner.style.display = "block"
    arrForChart = [];
    arrForChart.push(['Miejsca', 'Powtarzalnosć zajetych miejsc']);
    google.charts.load('current', { 'packages': ['corechart'] });
    google.setOnLoadCallback(function () {
        /* Ajax on Get */
        $.ajax({
            type: "GET",
            url: "/Statistics/StatisticsOverview?handler=PlaceInCompetitionAndCount",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: {
                "minDate": minDate != "" ? minDate : undefined,
                "maxDate": maxDate != "" ? maxDate : undefined,
            },
            success: function (data) {
                if (data == null || data.length == 0) {
                    arrForChart.push(["0", 0])
                    return drawChart(arrForChart);
                }

                data.forEach(function (element) {
                    arrForChart.push([element.zajeteMiejsce, element.powtarzalnoscMiejsca])
                });
                if (document.getElementById("StatisticsCustomcheckbox1").checked)
                    drawChart(arrForChart, "ComboChart", "Zawody ‒ zajęte miejsca", "Zajęte miejsce", "Częstotliwość");
                if (document.getElementById("StatisticsCustomcheckbox2").checked)
                    drawChart(arrForChart, "PieChart", "Zawody ‒ zajęte miejsca", "Zajęte miejsce", "Częstotliwość");
                if (document.getElementById("StatisticsCustomcheckbox3").checked)
                    drawChart(arrForChart, "AreaChart", "Zawody ‒ zajęte miejsca", "Zajęte miejsce", "Częstotliwość");
            },
            failure: function (response) {
                getAlertSlideUp("Ajax@google.charts.load(): Nieudana operacja");
            },
            error: function (response) {
                getAlertSlideUp("Ajax@google.charts.load(): Nieudana operacja");
            },
            complete: function (response) {
                spinner.style.display = "none";
            },
        });

    });
}


//ilosc akcji od danego okresu 
function getChartActionsYears_Count(minDate, maxDate) {
    spinner.style.display = "block"
    arrForChart = [];
    arrForChart.push(['Akcje', 'Ilośc akcji']);
    google.charts.load('current', { 'packages': ['corechart'] });
    google.setOnLoadCallback(function () {
        /* Ajax on Get */
        $.ajax({
            type: "GET",
            url: "/Statistics/StatisticsOverview?handler=ActionsYearsAndCount",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: {
                "yearOnly": document.getElementById("StatisticsCustomRadio2.5").checked ? true : false,
                "limitedScope": document.getElementById("StatisticsCustomcheckboxDataRange").checked ? true : false,
                "minDate": minDate != "" ? minDate : undefined,
                "maxDate": maxDate != "" ? maxDate : undefined,
            },
            success: function (data) {
                if (data == null || data.length == 0) {
                    arrForChart.push(["0", 0])
                    return drawChart(arrForChart);
                }

                data.forEach(function (element) {
                    arrForChart.push([element.czasZdarzenia, element.iloscZdarzen])
                });

                if (document.getElementById("StatisticsCustomcheckbox1").checked)
                    drawChart(arrForChart, "ComboChart", "Akcje ‒ suma w danym roku", "Rok", "Ilość");
                if (document.getElementById("StatisticsCustomcheckbox2").checked)
                    drawChart(arrForChart, "PieChart", "Akcje ‒ suma w danym roku", "Rok", "Ilość");
                if (document.getElementById("StatisticsCustomcheckbox3").checked)
                    drawChart(arrForChart, "AreaChart", "Akcje ‒ suma w danym roku", "Rok", "Ilość");
            },
            failure: function (response) {
                getAlertSlideUp("Ajax@google.charts.load(): Nieudana operacja");
            },
            error: function (response) {
                getAlertSlideUp("Ajax@google.charts.load(): Nieudana operacja");
            },
            complete: function (response) {
                spinner.style.display = "none";
            },
        });

    });
}


//ilosc akcji w jakich czlonkowie  brali udzial
function getChartMembersActions_Count(minDate, maxDate) {
    spinner.style.display = "block"
    arrForChart = [];
    arrForChart.push(['Akcje', 'Ilość akcji']);
    google.charts.load('current', { 'packages': ['corechart'] });
    google.setOnLoadCallback(function () {
        /* Ajax on Get */
        $.ajax({
            type: "GET",
            url: "/Statistics/StatisticsOverview?handler=MembersActionCount",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: {
                "minDate": minDate != "" ? minDate : undefined,
                "maxDate": maxDate != "" ? maxDate : undefined,
            },
            success: function (data) {
                if (data == null || data.length == 0) {
                    arrForChart.push(["0", 0])
                    $("#displayChart2").fadeOut(0);
                    return drawChart(arrForChart);
                }

                data.forEach(function (element) {
                    arrForChart.push([element.imie.charAt(0) + ". " + element.nazwisko, element.iloscAkcji])
                });
                if (document.getElementById("StatisticsCustomcheckbox1").checked)
                    drawChart(arrForChart, "ComboChart", "Członkowie ‒ uczestnictwo w akcjach", "Członek", "Ilość");
                if (document.getElementById("StatisticsCustomcheckbox2").checked)
                    drawChart(arrForChart, "PieChart", "Członkowie ‒ uczestnictwo w akcjach", "Członek", "Ilość");
                if (document.getElementById("StatisticsCustomcheckbox3").checked)
                    drawChart(arrForChart, "AreaChart", "Członkowie ‒ uczestnictwo w akcjach", "Członek", "Ilość");
            },
            failure: function (response) {
                getAlertSlideUp("Ajax@google.charts.load(): Nieudana operacja");
            },
            error: function (response) {
                getAlertSlideUp("Ajax@google.charts.load(): Nieudana operacja");
            },
            complete: function (response) {
                spinner.style.display = "none";
            },
        });

    });
}


//sumaryczny czas jaki uzytkownicy spedzili w akcji
function getChartMembersActions_Time(minDate, maxDate) {
    spinner.style.display = "block"
    arrForChart = [];
    arrForChart.push(['Czlonkowie', 'Czas']);
    google.charts.load('current', { 'packages': ['corechart'] });
    google.setOnLoadCallback(function () {
        /* Ajax on Get */
        $.ajax({
            type: "GET",
            url: "/Statistics/StatisticsOverview?handler=MembersActionCount",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: {
                "minDate": minDate != "" ? minDate : undefined,
                "maxDate": maxDate != "" ? maxDate : undefined,
            },
            success: function (data) {
                if (data == null || data.length == 0) {
                    arrForChart.push(["0", 0])
                    return drawChart(arrForChart);
                }

                data.forEach(function (element) {
                    arrForChart.push([element.imie.charAt(0) + ". " + element.nazwisko,
                    parseFloat((element.czasSuma / 60).toFixed(2))])
                });
                if (document.getElementById("StatisticsCustomcheckbox1").checked)
                    drawChart(arrForChart, "ComboChart", "Członkowie ‒ czas w akcjach", "Członek", "Czas [h]");
                if (document.getElementById("StatisticsCustomcheckbox2").checked)
                    drawChart(arrForChart, "PieChart", "Członkowie ‒ czas w akcjach", "Członek", "Czas[h]");
                if (document.getElementById("StatisticsCustomcheckbox3").checked)
                    drawChart(arrForChart, "AreaChart", "Członkowie ‒ czas w akcjach", "Członek", "Czas[h]");
            },
            failure: function (response) {
                getAlertSlideUp("Ajax@google.charts.load(): Nieudana operacja");
            },
            error: function (response) {
                getAlertSlideUp("Ajax@google.charts.load(): Nieudana operacja");
            },
            complete: function (response) {
                spinner.style.display = "none";
            },
        });

    });
}






function getChartTransactions_Time(minDate, maxDate) {
    spinner.style.display = "block"
    arrForChart = [];
    arrForChart.push(['Data', 'Sumaryczna kwota wpływów']);
    google.charts.load('current', { 'packages': ['corechart'] });
    google.setOnLoadCallback(function () {
        /* Ajax on Get */
        $.ajax({
            type: "GET",
            url: "/Statistics/StatisticsOverview?handler=TransactionsSumYears",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: {
                "yearOnly": document.getElementById("StatisticsCustomRadio5.5").checked ? true : false,
                "limitedScope": document.getElementById("StatisticsCustomcheckboxDataRange").checked ? true : false,
                "minDate": minDate != "" ? minDate : undefined,
                "maxDate": maxDate != "" ? maxDate : undefined,
            },
            success: function (data) {

                if (data == null || data.length == 0) {
                    arrForChart.push(["0", 0])
                    return drawChart(arrForChart);
                }

                data.forEach(function (element) {

                    arrForChart.push([element.rokTransakcji,
                    parseInt(element.kwotaTransakcji)])
                });
                if (document.getElementById("StatisticsCustomcheckbox1").checked)
                    drawChart(arrForChart, "ComboChart", "Transakcje ", "Data", "Kwota");
                if (document.getElementById("StatisticsCustomcheckbox2").checked)
                    drawChart(arrForChart, "PieChart", "Transakcje ", "Data", "Kwota");
                if (document.getElementById("StatisticsCustomcheckbox3").checked)
                    drawChart(arrForChart, "AreaChart", "Transakcje ", "Data", "Kwota");
            },
            failure: function (response) {
                getAlertSlideUp("Ajax@google.charts.load(): Nieudana operacja");
            },
            error: function (response) {
                getAlertSlideUp("Ajax@google.charts.load(): Nieudana operacja");
            },
            complete: function (response) {
                spinner.style.display = "none";
            },
        });

    });
}


function getChartFinances_Time(minDate, maxDate) {
    spinner.style.display = "block"
    arrForChart = [];
    arrForChart.push(['Data', 'Stan konta']);
    google.charts.load('current', { 'packages': ['corechart'] });
    google.setOnLoadCallback(function () {
        /* Ajax on Get */
        $.ajax({
            type: "GET",
            url: "/Statistics/StatisticsOverview?handler=FinansesSumYears",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: {
                "yearOnly": document.getElementById("StatisticsCustomRadio7").checked ? true : false,
                "limitedScope": document.getElementById("StatisticsCustomcheckboxDataRange").checked ? true : false,
                "minDate": minDate != "" ? minDate : undefined,
                "maxDate": maxDate != "" ? maxDate : undefined,
            },
            success: function (data) {

                if (data == null || data.length == 0) {
                    arrForChart.push(["0", 0])
                    return drawChart(arrForChart);
                }

                data.forEach(function (element) {

                    arrForChart.push([element.rok,
                    parseInt(element.suma)])
                });
                if (document.getElementById("StatisticsCustomcheckbox1").checked)
                    drawChart(arrForChart, "ComboChart", "Stan konta ", "Data", "Kwota");
                if (document.getElementById("StatisticsCustomcheckbox2").checked)
                    drawChart(arrForChart, "PieChart", "Stan konta ", "Data", "Kwota");
                if (document.getElementById("StatisticsCustomcheckbox3").checked)
                    drawChart(arrForChart, "AreaChart", "Stan konta ", "Data", "Kwota");
            },
            failure: function (response) {
                getAlertSlideUp("Ajax@google.charts.load(): Nieudana operacja");
            },
            error: function (response) {
                getAlertSlideUp("Ajax@google.charts.load(): Nieudana operacja");
            },
            complete: function (response) {
                spinner.style.display = "none";
            },
        });

    });
}






function drawChart(dataArr, charType, mainTitle, xName, yName) {
    var data = google.visualization.arrayToDataTable(dataArr)
    var optionsComboChart = {
        legend: 'top',
        //width: $(window).width() * 0.9,
        colors: ['#0087ff'],
        height: 450,
        bar: { groupWidth: '90%' },
        chartArea: { width: "85%", height: "75%" },
        title: mainTitle,
        seriesType: "bars",
        series: { 1: { type: "line" } },
        hAxis: {
            title: xName,
            //minValue:0, maxValue:10,
            titleTextStyle: { color: 'black', fontSize: 14, bold: true, italic: false },
            format: '0'
        },
        vAxis: {
            title: yName,
            //minValue:0, maxValue:10,
            titleTextStyle: { color: 'black', fontSize: 14, bold: true, italic: false },
            format: '0'
        },
        //colors: ['pink', 'yellow' ,'green' , 'black']
        animation: {
            duration: 1500,
            easing: 'out',
            startup: true //on start
        },
    };


    var optionsPieChart = {
        legend: 'bottom',
        //is3D: true,
        pieHole: 0.25,
        //pieSliceText: 'label',
        pieSliceText: 'percentage',
        //slices: { 2: { offset: 0.2 }, },
        //width: $(window).width() * 0.9,
        height: 450,
        bar: { groupWidth: '70%' },
        chartArea: { width: "90%", height: "75%" },
        title: mainTitle,
        seriesType: "bars",
        series: { 1: { type: "line" } },
        hAxis: {
            title: xName,
            //minValue:0, maxValue:10,
            titleTextStyle: { color: 'black', fontSize: 14, bold: true, italic: false },
            format: '0'
        },
        vAxis: {
            title: yName,
            //minValue:0, maxValue:10,
            titleTextStyle: { color: 'black', fontSize: 14, bold: true, italic: false },
            format: '0'
        },
        //colors: ['pink', 'yellow' ,'green' , 'black']

    };

    var optionsAreaChart = {
        legend: 'top',
        title: mainTitle,
        hAxis: {
            title: xName,
            titleTextStyle: { color: 'black', fontSize: 14, bold: true, italic: false },
            format: '0'
        },
        vAxis: {
            title: yName,
            titleTextStyle: { color: 'black', fontSize: 14, bold: true, italic: false },
            format: '0'
        },
        height: 450,
        colors: ['#0087ff', 'blue', '#3fc26b'],
        chartArea: { width: "85%", height: "75%" },
        animation: {
            duration: 1500,
            easing: 'out',
            startup: true //on start
        }
    };


    if (charType == "PieChart") {
        $("#displayChart2").fadeOut(0)
        $("#displayChart2").fadeIn(500)
        var chart = new google.visualization.PieChart(document.getElementById('displayChart2'));

        chart.clearChart();
        chart.draw(data, optionsPieChart);
    }
    else if (charType == "ComboChart") {
        $("#displayChart").fadeIn(100)
        var chart = new google.visualization.ComboChart(document.getElementById('displayChart'));

        chart.clearChart();
        chart.draw(data, optionsComboChart);
    }

    else if (charType == "AreaChart") {
        $("#displayChart3").fadeIn(100)
        var chart = new google.visualization.AreaChart(document.getElementById('displayChart3'));

        chart.clearChart();
        chart.draw(data, optionsAreaChart);
    }


}




//for resizing chart
function autoResizeChart() {
    $(window).resize(function () {
        if ($(window).width() < 550) {

            if (document.getElementById("StatisticsCustomRadio1").checked) {
                if (document.getElementById("StatisticsCustomcheckbox1").checked)
                    drawChart(arrForChart, "ComboChart", "Zawody ‒ zajęte miejsca", "Zajęte miejsce", "Częstotliwość");
                if (document.getElementById("StatisticsCustomcheckbox2").checked)
                    drawChart(arrForChart, "PieChart", "Zawody ‒ zajęte miejsca", "Zajęte miejsce", "Częstotliwość");
                if (document.getElementById("StatisticsCustomcheckbox3").checked)
                    drawChart(arrForChart, "AreaChart", "Zawody ‒ zajęte miejsca", "Zajęte miejsce", "Częstotliwość");
            }
            else if (document.getElementById("StatisticsCustomRadio2").checked) {
                if (document.getElementById("StatisticsCustomcheckbox1").checked)
                    drawChart(arrForChart, "ComboChart", "Akcje ‒ suma w danym roku", "Rok", "Ilość");
                if (document.getElementById("StatisticsCustomcheckbox2").checked)
                    drawChart(arrForChart, "PieChart", "Akcje ‒ suma w danym roku", "Rok", "Ilość");
                if (document.getElementById("StatisticsCustomcheckbox3").checked)
                    drawChart(arrForChart, "AreaChart", "Akcje ‒ suma w danym roku", "Rok", "Ilość");

            }
            else if (document.getElementById("StatisticsCustomRadio2.5").checked) {
                if (document.getElementById("StatisticsCustomcheckbox1").checked)
                    drawChart(arrForChart, "ComboChart", "Akcje ‒ suma w danym roku", "Rok", "Ilość");
                if (document.getElementById("StatisticsCustomcheckbox2").checked)
                    drawChart(arrForChart, "PieChart", "Akcje ‒ suma w danym roku", "Rok", "Ilość");
                if (document.getElementById("StatisticsCustomcheckbox3").checked)
                    drawChart(arrForChart, "AreaChart", "Akcje ‒ suma w danym roku", "Rok", "Ilość");

            }
            else if (document.getElementById("StatisticsCustomRadio3").checked) {
                if (document.getElementById("StatisticsCustomcheckbox1").checked)
                    drawChart(arrForChart, "ComboChart", "Członkowie ‒ uczestnictwo w akcjach", "Członek", "Ilość");
                if (document.getElementById("StatisticsCustomcheckbox2").checked)
                    drawChart(arrForChart, "PieChart", "Członkowie ‒ uczestnictwo w akcjach", "Członek", "Ilość");
                if (document.getElementById("StatisticsCustomcheckbox3").checked)
                    drawChart(arrForChart, "AreaChart", "Członkowie ‒ uczestnictwo w akcjach", "Członek", "Ilość");
            }
            else if (document.getElementById("StatisticsCustomRadio4").checked) {
                if (document.getElementById("StatisticsCustomcheckbox1").checked)
                    drawChart(arrForChart, "ComboChart", "Członkowie ‒ czas w akcjach", "Członek", "Czas [h]");
                if (document.getElementById("StatisticsCustomcheckbox2").checked)
                    drawChart(arrForChart, "PieChart", "Członkowie ‒ czas w akcjach", "Członek", "Czas[h]");
                if (document.getElementById("StatisticsCustomcheckbox3").checked)
                    drawChart(arrForChart, "AreaChart", "Członkowie ‒ czas w akcjach", "Członek", "Czas[h]");
            }
            else if (document.getElementById("StatisticsCustomRadio5").checked) {
                if (document.getElementById("StatisticsCustomcheckbox1").checked)
                    drawChart(arrForChart, "ComboChart", "Transakcje ", "Data", "Kwota");
                if (document.getElementById("StatisticsCustomcheckbox2").checked)
                    drawChart(arrForChart, "PieChart", "Transakcje ", "Data", "Kwota");
                if (document.getElementById("StatisticsCustomcheckbox3").checked)
                    drawChart(arrForChart, "AreaChart", "Transakcje ", "Data", "Kwota");
            }

            else if (document.getElementById("StatisticsCustomRadio5.5").checked) {
                if (document.getElementById("StatisticsCustomcheckbox1").checked)
                    drawChart(arrForChart, "ComboChart", "Transakcje ", "Data", "Kwota");
                if (document.getElementById("StatisticsCustomcheckbox2").checked)
                    drawChart(arrForChart, "PieChart", "Transakcje ", "Data", "Kwota");
                if (document.getElementById("StatisticsCustomcheckbox3").checked)
                    drawChart(arrForChart, "AreaChart", "Transakcje ", "Data", "Kwota");
            }
            else if (document.getElementById("StatisticsCustomRadio6").checked) {
                if (document.getElementById("StatisticsCustomcheckbox1").checked)
                    drawChart(arrForChart, "ComboChart", "Stan konta ", "Data", "Kwota");
                if (document.getElementById("StatisticsCustomcheckbox2").checked)
                    drawChart(arrForChart, "PieChart", "Stan konta ", "Data", "Kwota");
                if (document.getElementById("StatisticsCustomcheckbox3").checked)
                    drawChart(arrForChart, "AreaChart", "Stan konta ", "Data", "Kwota");
            }
            else if (document.getElementById("StatisticsCustomRadio7").checked) {
                if (document.getElementById("StatisticsCustomcheckbox1").checked)
                    drawChart(arrForChart, "ComboChart", "Stan konta ", "Data", "Kwota");
                if (document.getElementById("StatisticsCustomcheckbox2").checked)
                    drawChart(arrForChart, "PieChart", "Stan konta ", "Data", "Kwota");
                if (document.getElementById("StatisticsCustomcheckbox3").checked)
                    drawChart(arrForChart, "AreaChart", "Stan konta ", "Data", "Kwota");
            }
        }

    });
}






function selectedStatisticsToDisplay() {

    document.getElementById("displayChart").innerHTML = "";
    document.getElementById("displayChart2").innerHTML = "";
    document.getElementById("displayChart3").innerHTML = "";

    if (!document.getElementById("StatisticsCustomcheckbox1").checked
        && !document.getElementById("StatisticsCustomcheckbox2").checked
        && !document.getElementById("StatisticsCustomcheckbox3").checked) {
        return;
    }

    minDate = document.getElementById("MinStatisticsDateInput").value;
    maxDate = document.getElementById("MaxStatisticsDateInput").value;
    if (maxDate == "") maxDate = prepareDateNowForInput();

    document.getElementById("displayChartDate").innerText =
        (minDate != "" || maxDate != "") ?
            "📅 " + minDate + " ‒ " + maxDate : "";

    if (document.getElementById("StatisticsCustomRadio1").checked) {
        getChartCompetitionPlace_Count(minDate, maxDate);
        document.cookie = "StatisticsRadioNum= 1";
    }
    else if (document.getElementById("StatisticsCustomRadio2").checked) {
        getChartActionsYears_Count(minDate, maxDate);
        document.cookie = "StatisticsRadioNum= 2";
    }
    else if (document.getElementById("StatisticsCustomRadio2.5").checked) {
        getChartActionsYears_Count(minDate, maxDate);
        document.cookie = "StatisticsRadioNum= 2.5";
    }
    else if (document.getElementById("StatisticsCustomRadio3").checked) {
        getChartMembersActions_Count(minDate, maxDate);
        document.cookie = "StatisticsRadioNum= 3";
    }
    else if (document.getElementById("StatisticsCustomRadio4").checked) {
        getChartMembersActions_Time(minDate, maxDate);
        document.cookie = "StatisticsRadioNum= 4";
    }
    else if (document.getElementById("StatisticsCustomRadio5").checked) {
        getChartTransactions_Time(minDate, maxDate);
        document.cookie = "StatisticsRadioNum= 5";
    }

    else if (document.getElementById("StatisticsCustomRadio5.5").checked) {
        getChartTransactions_Time(minDate, maxDate);
        document.cookie = "StatisticsRadioNum= 5.5";
    }

    else if (document.getElementById("StatisticsCustomRadio6").checked) {
        getChartFinances_Time(minDate, maxDate);
        document.cookie = "StatisticsRadioNum= 6";
    }

    else if (document.getElementById("StatisticsCustomRadio7").checked) {
        getChartFinances_Time(minDate, maxDate);
        document.cookie = "StatisticsRadioNum= 7";
    }

}


function clearStatisticsDropDownDate(element) {
    document.getElementById(element).value = "";
}




//##########################################################Finances.FinancesOverview
function onloadFinances() {
    var filter = getCookie("FilterFinancesOverview");
    var sort = getCookie("SortFinancesOverview");

    if (filter == "Wplywy")
        document.getElementById("FinancesCustomRadioFilter1").checked = true;
    else if (filter == "Wydatki")
        document.getElementById("FinancesCustomRadioFilter2").checked = true;
    else if (filter == "Korekty")
        document.getElementById("FinancesCustomRadioFilter3").checked = true;
    else if (filter == "all")
        document.getElementById("FinancesCustomRadioFilter4").checked = true;


    if (sort == "Kwota")
        document.getElementById("FinancesCustomRadioSort1").checked = true;
    else if (sort == "Data")
        document.getElementById("FinancesCustomRadioSort2").checked = true;
}




//Gets row and column number of click
$("#financesTable td").click(function () {

    var thisTable = document.getElementById("financesTable");
    var columnNum = parseInt($(this).index()) + 1;
    var rowNum = parseInt($(this).parent().index());
    var hiddenIdValue = thisTable.rows[rowNum].cells[0].lastElementChild.value;

    var newLocation = location.toString()
    newLocation = newLocation.substring(0, newLocation.indexOf('FinancesOverview'));
    newLocation += "ViewFinances?selectedFinancesId=" + hiddenIdValue;
    window.location.href = newLocation;

    //var newLocation = location.toString().replace("RemindersOverview", "ViewReminder?selectedReminderId=" + hiddenIdValue);
    //window.location.href = newLocation;
});

function financesKorektaClick() {
    if (document.getElementById("FinancesSumInput").style.display == "none") {
        document.getElementById("FinancesSumDisplay").style.display = "none";
        document.getElementById("FinancesSumInput").style.display = "block";
    }
    else if (document.getElementById("FinancesSumDisplay").style.display == "none") {
        document.getElementById("FinancesSumDisplay").style.display = "block";
        document.getElementById("FinancesSumInput").style.display = "none";
    }
}


function submitNewFinancesSum() {

    spinner.style.display = "block"
    $.ajax({
        type: "POST",
        url: "/Finances/FinancesOverview?handler=EditFinancesSum",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: {
            "oldSum": document.getElementById("currentMoneySumHiddenInp").value,
            "newSum": document.getElementById("NewFinancesSumInput").value,
        },
        success: function (data) {
            var newLocation = location.toString()
            window.location.href = newLocation + "?passAlert=" + data;
        },

        failure: function (response) {
            getAlertSlideUp("Ajax@editAction(): Nieudana operacja");
        },
        error: function (response) {
            getAlertSlideUp("Ajax@editAction(): Nieudana operacja");
        },
        complete: function (response) {
            spinner.style.display = "none";
            financesKorektaClick();
        },
    });
}





//##########################################################QRCode.QRCodeScaner




//##########################################################Restricted.RestrictedMain
function restrictedMainOnload() {

    connection.start();
     //for Chat count in RestrictedMain
    connection.on("ReceiveMessage", function () {
        let chatCount4Restricted = parseInt(document.getElementById("restrictedChatnotiDot").innerText);
        document.getElementById("restrictedChatnotiDot").innerText = chatCount4Restricted + 1;
    });
    //for Annoucement count in RestrictedMain
    connection.on("ReceiveMessageSec", function (title, dateX) {
        let chatCount4Restricted = parseInt(document.getElementById("restrictedAnnouncementnotiDot").innerText);
        document.getElementById("restrictedAnnouncementnotiDot").innerText = chatCount4Restricted + 1;
        let ul = document.getElementById("announcementListIdent");
     

        var li = document.createElement("li");
        li.appendChild(document.createTextNode(dateX));
        li.classList.add("restrictedAnnouncementList");
        ul.insertBefore(li, ul.firstChild);

        var li = document.createElement("li");
        li.appendChild(document.createTextNode(title));
        li.classList.add("mt-1")
        ul.insertBefore(li, ul.firstChild);
    });

}

function onRestrictedMainStatusSwich() {
    spinner.style.display = "block"
    $.ajax({
        type: "POST",
        url: "/Restricted/RestrictedMain?handler=UpdateMemberStatus",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: {
            dostepny: document.getElementById("restrictedMainStatusSwich").checked
        },
        success: function (data) {
            document.getElementById("displayStatusR").innerText =
                document.getElementById("restrictedMainStatusSwich").checked ?
                "Dostępny" : "Niedostępny"
        },

        failure: function (response) {
            getAlertSlideUp("Ajax@editAction(): Nieudana operacja");
        },
        error: function (response) {
            getAlertSlideUp("Ajax@editAction(): Nieudana operacja");
        },
        complete: function (response) {
            spinner.style.display = "none";
        },
    });
}





/* ##################################################################### */
/* ############################## Calendar ############################# */
/* ##################################################################### */

function calendarOnload() {
    document.getElementById("selectedMonth").value = monthNum[currentMonthNumber];
    document.getElementById("selectedYear").value = currentYear;
    updateDayMonthYearCalendarField();
    updateCalendarFieldSize();
    new ResizeObserver(updateCalendarFieldSize).observe(calendar);

    /* Added e.preventDefault() which prevents website from redirecting (reloading page) and as event I used submit */
    $("#addReminderWindow").on("submit", function (e) {
        e.preventDefault()

        addNewEvent();
    });


}

function updateCalendarFieldSize() {
    //calendar.offsetWidth
    var dayFields = $(".dayFieldClass");
    var disabledFields = $(".disabledDayFieldClass");


    if (dayFields.width() < 80) {
        dayFields.css("height", dayFields.width());
        disabledFields.css("height", disabledFields.width());
        $(".reminderTitleCalendarField").parent(".dayFieldClass").children('.innerCalendarFieldDiv').css("display", "block")
        $(".reminderTitleCalendarField").css("display", "none")
        $(".dayFieldClass").css("font-size", "12px");
        $(".dayFieldClass").css("overflow", "hidden");
    }
    else {
        dayFields.css("height", dayFields.width() * 0.75);
        disabledFields.css("height", disabledFields.width() * 0.75);
        $(".reminderTitleCalendarField").css("display", "block");
        $(".innerCalendarFieldDiv").css("display", "none")
        $(".dayFieldClass").css("font-size", "16px");
        $(".dayFieldClass").css("overflow", "auto");
    }
}




monthNum = ["styczen", "luty", "marzec", "kwiecien", "maj", "czerwiec", "lipiec",
    "sierpien", "wrzesien", "pazdziernik", "listopad", "grudzien"];

dayOfWeek = ["Poniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek", "Sobota", "Niedziela"];
var choosenDay;
var eventsList = "";
var currentDate = new Date()
var currentMonthNumber = currentDate.getMonth();
var currentYear = currentDate.getFullYear();


class monthCls {
    static getDayAmount(month, year) {
        if (month == monthNum[0] || month == monthNum[2] || month == monthNum[4]
            || month == monthNum[6] || month == monthNum[7] || month == monthNum[9]
            || month == monthNum[11]) {
            return "31";
        }
        else if (month == monthNum[3] || month == monthNum[5]
            || month == monthNum[8] || month == monthNum[10]) {
            return 30;
        }
        else if (month == monthNum[1] && (year % 4 == 0 && year % 100 != 0)
            || year % 400 == 0) {
            return 29;
        }
        else {
            return 28;
        }
    }
    static getNextMonth(month) {
        if (month == "grudzien") {
            return monthNum[0];
        }
        else {
            return monthNum[monthNum.indexOf(month) + 1]
        }
    }
    static getPreviousMonth(month) {
        if (month == "styczen") {
            return monthNum[11];
        }
        else {
            return monthNum[monthNum.indexOf(month) - 1]
        }
    }
}




function createCalendar(daysAmount = 31) {



    var selectedMonthField = document.getElementById("selectedMonth").value;
    var selectedMonthNumber = monthNum.indexOf(selectedMonthField) + 1;
    selectedMonthNumber = selectedMonthNumber < 10 ? "0" + selectedMonthNumber : selectedMonthNumber;
    var selectedYearField = document.getElementById("selectedYear").value;

    document.getElementById("calendarWait").style.display = "block";



    //insertIntoCalendarFIeld("dayFieldId1", "▐  sassdsdss dsds");



    /* Ajax on Get // Gets All events */
    var options = {};
    options.url = "/Calendar/CalendarView?handler=SelectEventsByPartDate";
    options.type = "GET";
    options.dataType = "json";
    options.data = { dateElement: "month", partDate: selectedMonthNumber };
    options.success = function (data) {





        var el = document.getElementById('calendar');
        while (el.firstChild) el.removeChild(el.firstChild);

        //day names on top
        for (let z = 0; z < dayOfWeek.length; z++) {
            let field = document.getElementById("calendar").appendChild(document.createElement("div"));
            field.className = 'calendarTopDayName';
            field.innerText = dayOfWeek[z];
        }

        //for first disabled days in month
        for (let j = 0; j < getFirstDayNumberOfMonth(selectedMonthNumber, selectedYearField) - 1; j++) {
            let field = document.getElementById("calendar").appendChild(document.createElement("div"));
            field.className = 'disabledDayFieldClass';
        }

        //days of given month
        for (let i = 1; i < daysAmount + 1; i++) {
            let field = document.getElementById("calendar").appendChild(document.createElement("div"));
            field.className = ((i < 10) ? "0" + i : i) + "." + selectedMonthNumber +
                "." + selectedYearField + ' dayFieldClass ';
            field.classList.add(selectedMonthNumber + "." + ((i < 10) ? "0" + i : i) +  //laternative date format for iphone
                "." + selectedYearField);
            field.id = "dayFieldId" + i;
            field.innerHTML = "<span>" + i + "\xa0</span>" ;
            field.onclick = calendarFieldClick;
            field.onmouseover = mouseOver;
            field.onmouseout = mouseOut;
            //4 notification
            let innerField = field.appendChild(document.createElement("div"));
            innerField.className = "innerCalendarFieldDiv";
            


            data.forEach(function (element) {

                var rawDate = new Date(element.dataRozpoczecia);
                var MonthNumberOnly = rawDate.getMonth() + 1;
                MonthNumberOnly = MonthNumberOnly < 10 ? "0" + MonthNumberOnly : MonthNumberOnly;
                var yearOnly = rawDate.getFullYear();
                var dayOnly = rawDate.getDate();
                dayOnly = dayOnly < 10 ? "0" + dayOnly : dayOnly;
                var preparedDate = dayOnly + "." + MonthNumberOnly + "." + yearOnly;
                
                //if (field.classList.contains(element.dataRozpoczecia.slice(0, 10).replaceAll('/', '.'))) {
                if (field.classList.contains(preparedDate) ||
                    field.classList.contains(element.dataRozpoczecia.slice(0, 10).replaceAll('/', '.'))) { //for mobile
                    insertIntoCalendarFIeld(field.id, "▐ " + element.tytul, element.tresc, element.czcionkaHex);
                    field.style.backgroundColor = element.tloHex;
                }

            });

        }
        updateCalendarFieldSize();
        document.getElementById("calendarWait").style.display = "none";



    };
    options.error = function () {
        alert("Ajax@createCalendar(): Nieudana operacja");
    };
    $.ajax(options);

}


//day of the week where the new month starts
function getFirstDayNumberOfMonth(month = new Date().getMonth() + 1, year = new Date().getFullYear()) {
    var dayOfWeek = new Date(year + "-" + month + "-01").getDay();
    dayOfWeek = (dayOfWeek == 0) ? 7 : dayOfWeek;
    return dayOfWeek;
}


function mouseOver() {
    document.getElementById(this.id).style.borderColor = "#a2a4a7"
}


function mouseOut() {
    document.getElementById(this.id).style.borderColor = "#fff"
}


//add new insert into given month field
function insertIntoCalendarFIeld(fieldId, title, content, fontColor) {
    var field = document.getElementById(fieldId).appendChild(document.createElement("div"));
    field.className = 'reminderTitleCalendarField';
    field.innerText = title;
    field.style.color = fontColor;
    var field = document.getElementById(fieldId).appendChild(document.createElement("input"));
    field.innerText = content;
    field.type = "hidden"
}

function calendarFieldClick() {
    var selectedYear = document.getElementById("selectedYear").value;
    var selectedMonth = monthNum.indexOf(document.getElementById("selectedMonth").value) + 1;
    var selectedDay = this.id.replace('dayFieldId', '');
    selectedMonth = selectedMonth < 10 ? '0' + selectedMonth : selectedMonth;
    selectedDay = selectedDay < 10 ? '0' + selectedDay : selectedDay;
    var selectedDate = selectedDay + '.' + selectedMonth + '.' + selectedYear;






    /* Ajax on Get // Gets All events */
    var options = {};
    options.url = "/Calendar/CalendarView?handler=SelectEventsByDate";
    options.type = "GET";
    options.dataType = "json";
    options.data = { dateX: selectedDate };
    options.success = function (data) {



        var el = document.getElementById("viewCalendarReminderWindow");
        el.innerHTML = '';
        if (data.length == 0)
            el.innerHTML = '<h2 class="centerVerticalHorizontal">Brak</h2>';


        data.forEach(function (element) {

            var notifGraph1 = "";
            var notifGraph2 = "";
            if (element.powiadomienieTelefon == "t") {
                notifGraph1 = '<div class="mr-2"><img src="../img/black/phone-vibrate.svg"></div>';
            }
            if (element.powiadomienieEmail == "t") {
                notifGraph2 = '<div><img src="../img/black/envelope-check.svg"></div>';
            }
            el.innerHTML +=
                '<div class="viewEventListWin">'
                + '<div class="input-group mb-0">'
                + '<p type="text" class=" bg-transparent">'
                + "🕑 " + element.czasRozpoczecia + '</p>'
                + '<div class="ml-3 row">'+notifGraph1 + notifGraph2+'</div>'
                + '<input class="editBtnCalendarPage  btn btn-sm btn-dark"'
                + 'type="button" value="Usuń" id=' + "calEv" + element.id
                + ' onclick= "delReminder(this)"/> '
                + '</div>'

                + '<label for="reminderTitleInput" class="mb-0">Tytuł</label>'
                + '<input type="text" class="form-control bg-light reminderTitleInputView"'
                + 'readonly value="' + element.tytul + '">'

                + '<label for="reminderContentInput" class="mb-0 ">Treść</label>'
                + '<textarea class="form-control bg-light reminderContentInputView "'
                + 'readonly  rows="3" >' + element.tresc + '</textarea>'

                + '</div >'

        });


    };
    options.error = function () {
        alert("Ajax@calendarFieldClick(): Nieudana operacja");
    };
    $.ajax(options);




    var selectedDateV2 = selectedYear + '-' + selectedMonth + '-' + selectedDay;
    var currentTime = new Date().getHours() + ":" + new Date().getMinutes();
    document.getElementById("calendarDateInput").value = selectedDateV2;
    document.getElementById("calendarTimeInput").value = currentTime;


    $("#aboveCalendarWindow").fadeIn(600)
    document.getElementById("calendarFrame").style.filter = 'blur(5px)';
    document.getElementById("calendarFrame").style.pointerEvents = "none";

}

function delReminder(sender) {


    var selectedReminderId = sender.id.replace('calEv', '')

    $.ajax({
        type: "POST",
        url: "/Calendar/CalendarView?handler=RemoveByIdAjax",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: {
            "delNotiId": selectedReminderId,
        },
        success: function (data) {
            getAlertSlideUp(data);
            updateDayMonthYearCalendarField();
            closeOverCalendarWindow();

            //location.reload();
            //return false;

        },

        failure: function (response) {
            alert("Ajax@delReminder(): Nieudana operacja")
        },
        error: function (response) {
            alert("Ajax@delReminder(): Nieudana operacja")
        }
    });


}

function closeOverCalendarWindow() {
    $("#aboveCalendarWindow").fadeOut(600);
    document.getElementById("calendarFrame").style.filter = 'blur(0)';
    document.getElementById("calendarFrame").style.pointerEvents = "";
    var el = document.getElementById("viewCalendarReminderWindow");
    el.innerHTML = "";
    updateCalendarFieldSize();
}


//month selector on calendar page
function chooseMonth(sender) {
    let selectedMonthField = document.getElementById("selectedMonth");
    let selectedYearField = document.getElementById("selectedYear");
    let nextVal
    if (sender.id == "subtractMonthBtn") {
        nextVal = monthCls.getPreviousMonth(selectedMonthField.value)
        if (nextVal == monthNum[11]) {
            selectedYearField.value--;
        }
    }
    else if (sender.id == "addMonthBtn") {
        nextVal = monthCls.getNextMonth(selectedMonthField.value)
        if (nextVal == monthNum[0]) {
            selectedYearField.value++;
        }
    }
    selectedMonthField.value = nextVal;
    updateDayMonthYearCalendarField();

}


//year selector on calendar page
function chooseYear(sender) {
    let selectedYearField = document.getElementById("selectedYear");
    let selectedMonthField = document.getElementById("selectedMonth");
    if (sender.id == "subtractYearBtn") {
        selectedYearField.value = parseInt(selectedYearField.value) - 1;
    }
    else if (sender.id == "addYearBtn") {
        selectedYearField.value = parseInt(selectedYearField.value) + 1;
    }
    updateDayMonthYearCalendarField();
}


//updates amount of day fields when month or year has been changed
function updateDayMonthYearCalendarField() {
    let selectedYearField = document.getElementById("selectedYear");
    let selectedMonthField = document.getElementById("selectedMonth")
    var amountOfDays = monthCls.getDayAmount(selectedMonthField.value, selectedYearField.value);
    createCalendar(parseInt(amountOfDays));
}


function calendarTabs(sender) {
    if (sender.id == "viewReminderTab") {
        sender.classList.add("active")
        document.getElementById("addReminderTab").classList.remove("active");
        document.getElementById("viewCalendarReminderWindow").style.display = "block";
        document.getElementById("addReminderWindow").style.display = "none";
    }
    else if (sender.id == "addReminderTab") {
        sender.classList.add("active")
        document.getElementById("viewReminderTab").classList.remove("active");
        document.getElementById("addReminderWindow").style.display = "block";
        document.getElementById("viewCalendarReminderWindow").style.display = "none";
    }
}

function calendarTabAddDirect() {
    document.getElementById("viewReminderTab").classList.remove("active");
    document.getElementById("addReminderWindow").style.display = "block";
    document.getElementById("viewCalendarReminderWindow").style.display = "none";
}

function userListForNewReminderVisibility() {

    if (document.getElementById("notificationPhone").checked ||
        document.getElementById("notificationEmail").checked) {
        $("#hiddenNewReminderListOfUser").fadeIn(700)
    }
    else {
        $("#hiddenNewReminderListOfUser").fadeOut(350)
    }
}



function addNewEvent() {


    var listOfRecipients = [];
    var table = document.getElementById("addUserTableForReminder");
    var lastRowIndex = table.rows.length - 1;

    for (let i = 0; i < lastRowIndex + 1; i++) {
        if (table.rows[i].cells[0].lastElementChild.checked == true)
            listOfRecipients.push(table.rows[i].cells[1].lastElementChild.value)
    }

    if ((document.getElementById("notificationEmail").checked ||
        document.getElementById("notificationPhone").checked) &&
        listOfRecipients.length == 0) {
        getAlertSlideUp("(🛆) Nie wybrano adresatów");
        return;
    }


    var timeInput = document.getElementById("calendarDateInput").value
        + " " + document.getElementById("calendarTimeInput").value;

    var newEvent = {
        //JednostkaId:
        Tytul: document.getElementById("reminderTitleInput").value,
        Tresc: document.getElementById("reminderContentInput").value,
        DataRozpoczecia: document.getElementById("calendarDateInput").value,
        CzasRozpoczecia: timeInput,
        PowiadomienieEmail: document.getElementById("notificationEmail").checked ? "t" : "n",
        PowiadomienieTelefon: document.getElementById("notificationPhone").checked ? "t" : "n",
        TloHex: document.getElementById("selectCalendarFieldColor").value,
        CzcionkaHex: document.getElementById("selectCalendarFontColor").value,
    };


    $.ajax({
        type: "POST",
        url: "/Calendar/CalendarView?handler=AddEventAjax",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: {
            "obj": newEvent,
            "listOfRecipients": listOfRecipients,
        },
        success: function (data) {
            //getAlertSlideUp(data);

            var reloadLocation = location.toString()

            reloadLocation += "?passAlert=" + data;
            window.location.href = reloadLocation;

            //location.reload();
            //return false; 
        },

        failure: function (response) {
            getAlertSlideUp("Ajax@addNewAction(): Nieudana operacja");
        },
        error: function (response) {
            getAlertSlideUp("Ajax@addNewAction(): Nieudana operacja");
        }
    });


}









//##########################################################Chat.ChatView
var doScroll = false;

function ChatOnload() {
    displayOldMsgs();
    watch4ScrollDown();

    //Disable the send button until connection is established.
    //document.getElementById("sendButton").disabled = true;

    connection.start().then(function () {
        document.getElementById("sendButton").disabled = false;
    }).catch(function (err) {
        return console.error(err.toString());
    });


    connection.on("ReceiveMessage", function (senderId, senderName, message) {
        insertListRow(senderName, message, "", senderId)
        updateLastVisit(); //Jesli pojawia sie nowa wiadomosc i uzytkonik jest nas stronie chatu, wyslana jest aktualizacja daty, w przeciwnym razie, nie
    });
}


//if scroll on top ,add next 50 messages from server
function chatOnScroll() {
    var element = document.getElementById('messagesDisplaYDiv');
    if (element.scrollTop == 0) {
        loadNumberOfRows += 50;
        displayOldMsgs(true);
    }
}



function insertListRow(sender, msg, msgDate = "", senderId = "", addingNextRows = false) {
    var senderLi = document.createElement("li");
    var messageContentLi = document.createElement("li");
    var dataLi = document.createElement("li");
    var div = document.createElement("div");

    senderLi.style.fontSize = "0.75em";
    senderLi.style.marginBottom = "0.3em";
    senderLi.style.marginTop = "1em";


    div.style.width = "fit-content";
    div.style.maxWidth = "96%";
    div.style.minWidth = "40px";
    div.style.backgroundColor = "#eaebec";
    div.style.padding = "0.6em";
    div.style.borderRadius = "12px";
    div.classList.add("customShadow1");
    if (senderId == document.getElementById("chatCurrentLoggedUserId").value) {
        div.style.backgroundColor = "#0087ff";
        div.style.color = "#fff";
    }

    dataLi.style.fontSize = "0.7em";
    dataLi.style.marginTop = "5px";


    //if scroll is on boottom(with some margin(-150)) before adding new li, scroll automaticly down, if not, do nothing
    doScroll = false;
    var element = document.getElementById('messagesDisplaYDiv');
    if (element.scrollHeight - element.scrollTop - 150 <= element.clientHeight
        && addingNextRows == false) {
        doScroll = true;
    }

    document.getElementById("messagesList").appendChild(senderLi);
    document.getElementById("messagesList").appendChild(div).append(messageContentLi, dataLi);

    if (msgDate == "") {
        var currentDate = prepareDateNowForInput();
        var time = new Date();
        var currentTime = time.getHours() + ":" + time.getMinutes() + ":" + time.getSeconds();
        msgDate = currentDate + " " + currentTime;
    }

    senderLi.textContent = `${"#" + senderId + " " + sender}`;
    messageContentLi.textContent = `${msg}`;
    dataLi.textContent = ` ${msgDate}`;
}



document.getElementById("sendChatButton").addEventListener("click", function (event) {
  

    var message = document.getElementById("messageInput").value;

    if (message == "") return;
    document.getElementById("messageInput").value = "";

    //only users with the same unit id will get the message
    var currentlyLoggedUnitId = document.getElementById("chatCurrentLoggedUnit").value
    var currentlyLoggedUserId = document.getElementById("chatCurrentLoggedUserId").value

    connection.invoke("SendMessage", message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();

   


    $.ajax({
        type: "POST",
        url: "/Chat/ChatView?handler=SaveMsg",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: {
            "msg": message,
        },
        success: function (data) {
            updateLastVisit(); //aby wyslana wiadomosc nie byla zaznaczona jako nieodczytana
        },
        failure: function (response) {
            getAlertSlideUp("Ajax@SaveChatMsg(): Nieudana operacja");
        },
        error: function (response) {
            getAlertSlideUp("Ajax@SaveChatMsg(): Nieudana operacja");
        }
    });
});


function watch4ScrollDown() {
    $('#messagesList').on('DOMNodeInserted', 'li', function (e) {
        //if scroll is on boottom before adding new li, scroll automaticly down, if not, do nothing
        if (doScroll) {
            setTimeout(function () {
                var element = document.getElementById('messagesDisplaYDiv');
                element.scrollTop = element.scrollHeight;
            }
                , 300);
        }
    });
}

var loadedRows = 0;
var loadNumberOfRows = 25;
function displayOldMsgs(addingNextRows = false) {
    spinner.style.display = "block"
    $.ajax({
        type: "GET",
        url: "/Chat/ChatView?handler=LoadMsgs",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: {
            numberOfRows: loadNumberOfRows,
        },
        success: function (data) {
            if (loadedRows < data.length) {


                var content = document.getElementById('messagesDisplaYDiv');
                var curScrollPos = content.scrollTop;
                var oldScroll = content.scrollHeight - content.clientHeight;

                loadedRows = data.length;
                document.getElementById("messagesList").innerHTML = "";

                data.forEach(function (element) {
                    insertListRow(element.imie + " " + element.nazwisko, element.tresc, element.data, element.uzytkownikId, addingNextRows)
                })


              //getting back to old vertical position
                var newScroll = content.scrollHeight - content.clientHeight; 
                content.scrollTop = curScrollPos + (newScroll - oldScroll);
            }
           
        },
        failure: function (response) {
            getAlertSlideUp("Ajax@SaveChatMsg(): Nieudana operacja");
        },
        error: function (response) {
            getAlertSlideUp("Ajax@SaveChatMsg(): Nieudana operacja");
        },
        complete: function (response) {
            spinner.style.display = "none";
        }
    });
}


function sendChatMsgOnEnterDown() {
    if (event.key === 'Enter') {
        document.getElementById("sendChatButton").click();
    }
}



//Jesli pojawia sie nowa wiadomosc i uzytkonik jest nas stronie chatu, wyslana jest aktualizacja daty, w przeciwnym razie, nie
function updateLastVisit() {
    $.ajax({
        type: "POST",
        url: "/Chat/ChatView?handler=AjaxUpdateLastVisitDate",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: {
        },
        success: function (data) {
        },

        failure: function (response) {
            alert("Ajax@updateLastVisit(): Nieudana operacja")
        },
        error: function (response) {
            alert("Ajax@updateLastVisit(): Nieudana operacja")
        }
    });
}



//##########################################################Announcement.AddAnnouncement
function onloadAnnouncement() {
   

    $("#addAnnouncementForm").on("submit", function (e) {
        e.preventDefault()
        saveSendAnnouncements();
    });

}

function addMemberAnnouncementTableOnClick(sender) {
    var table = document.getElementById("addAnnouncementUserTable");
    var thisCheckbox = table.rows[sender.rowIndex].cells[0].lastElementChild;

    setTimeout(function () {
        thisCheckbox.checked = thisCheckbox.checked ? false : true;

        if (thisCheckbox.checked)
            table.rows[sender.rowIndex].classList.add("selectedTableSmRow")
        else if (table.rows[sender.rowIndex].classList.contains("selectedTableSmRow"))
            table.rows[sender.rowIndex].classList.remove("selectedTableSmRow");

    }
        , 150);
}


/*adasfjsbfilsfisbfsofbsbfsfbosefobiesfoiesbfoiebfobsefobseofibsoeibfoiABSCabfes*/ 
function saveSendAnnouncements() {

    var files = $('#fileUpload').prop("files");
    formData = new FormData();
    formData.append("uploadedFile", files[0]);


    var table = document.getElementById("addAnnouncementUserTable");
    var lastRowIndex = table.rows.length - 1;

    for (let i = 0; i < lastRowIndex + 1; i++) {

        if (table.rows[i].cells[0].lastElementChild.checked == true) {

            UczestnikId = table.rows[i].cells[1].lastElementChild.value;
            formData.append("odbiorcy", UczestnikId)  //listOfRecipients
        }
    }


    var Announcement = {
        Tytul: document.getElementById("announcementTitleInput").value,
        Tresc: document.getElementById("announcementContentInput").value,
        TloHex: document.getElementById("selectannouncementFieldColor").value,
        CzcionkaHex: document.getElementById("selectAnnouncementFontColor").value,
    };


    for (var key in Announcement) {
        formData.append(key, Announcement[key]);
    }

    $.ajax({
        type: "POST",
        url: "/Announcement/AddAnnouncement?handler=AddAnnouncement",
        cache: false,
        contentType: false,
        processData: false,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: formData,
        success: function (data) {
             //for Annoucement count in RestrictedMain
            let titleOfAnnouncement = document.getElementById("announcementTitleInput").value
            let dateX = "new";
            connection.invoke("SendMessageSec", titleOfAnnouncement, dateX ).catch(function (err) {
                return console.error(err.toString());
            });

            var newLocation = location.toString()
            newLocation = newLocation.substring(0, newLocation.indexOf('Announcement'));
            newLocation += "Announcement/AnnouncementsOverview?passAlert=" + data;
            window.location.href = newLocation;

        },
        failure: function (response) {
            getAlertSlideUp("Ajax@saveSendAnnouncements(): Nieudana operacja");
        },
        error: function (response) {
            getAlertSlideUp("Ajax@saveSendAnnouncements(): Nieudana operacja");
        },
        complete: function (response) {
        }
    });
}
/*adasfjsbfilsfisbfsofbsbfsfbosefobiesfoiesbfoiebfobsefobseofibsoeibfoiABSCabfes*/




//##########################################################Announcement.AnnouncementOverview
function announcementBoxClick(sender) {
    if (sender.querySelector('.AnnouncementBoxArrow').style.display == "none") {
        sender.querySelector('.AnnouncementBoxArrow').style.display = ""
    }
    else {
        sender.querySelector('.AnnouncementBoxArrow').style.display = "none"
    }
}

function deleteAnnouncementBox(sender) {

   

    $.ajax({
        type: "POST",
        url: "/Announcement/AnnouncementsOverview?handler=AjaxDeleteAnnouncement",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: {
            idToRemove: sender,
        },
        success: function (data) {
            $(".innerContainer").load(location.href + " .innerContainer"); // reload div. Whitespace important

        },
        failure: function (response) {
            getAlertSlideUp("Ajax@SaveChatMsg(): Nieudana operacja");
        },
        error: function (response) {
            getAlertSlideUp("Ajax@SaveChatMsg(): Nieudana operacja");
        },
        complete: function (response) {
        }
    });
}







//ADDING IMAGE @USERDATA PAGE
function onUserDataImageSelected(event) {



    $("#userAccountLogoInput").fadeIn(70);
    var selectedFile = event.target.files[0];
    var reader = new FileReader();

    var imgElement = document.getElementById("logoImage");
    imgElement.title = selectedFile.name;

    reader.onload = function (event) {
        imgElement.src = event.target.result;
    };

    reader.readAsDataURL(selectedFile);
    imgElement.src = "";

    setTimeout(function () {
        $("#userAccountLogoInput").fadeOut(700);
    }
        , 600);





    //$("#userAccountLogoInput").animate({ "marginLeft": -$('#AccountUserDataModelDiV').width() + -$('#userAccountLogoInput').width() }, "slow");
    //$("#logoImage").animate({ "height": "-=300px" }, "slow");

}



function decodeOnce(codeReader, selectedDeviceId) {
    codeReader.decodeFromInputVideoDevice(selectedDeviceId, 'video').then((result) => {
        console.log(result)
       
        document.getElementById('result').textContent = result.text
    }).catch((err) => {
        console.error(err)
        document.getElementById('result').textContent = err
    })
}




function onloadQrScanner() {

    let selectedDeviceId;
    const codeReader = new ZXing.BrowserQRCodeReader();
    console.log('ZXing code reader initialized')

    codeReader.getVideoInputDevices()
        .then((videoInputDevices) => {
            const sourceSelect = document.getElementById('sourceSelect')
            selectedDeviceId = videoInputDevices[0].deviceId
            if (videoInputDevices.length >= 1) {
                videoInputDevices.forEach((element) => {
                    const sourceOption = document.createElement('option')
                    sourceOption.text = element.label
                    sourceOption.value = element.deviceId
                    sourceSelect.appendChild(sourceOption)
                })

                sourceSelect.onchange = () => {
                    selectedDeviceId = sourceSelect.value;
                };

                const sourceSelectPanel = document.getElementById('sourceSelectPanel')
                sourceSelectPanel.style.display = 'block'
            }

            document.getElementById('startButton').addEventListener('click', () => {

                decodeOnce(codeReader, selectedDeviceId);

                console.log(`Started decode from camera with id ${selectedDeviceId}`)
            })

            document.getElementById('resetButton').addEventListener('click', () => {
                codeReader.reset()
                document.getElementById('result').textContent = '';
                console.log('Reset.')
            })

        })
        .catch((err) => {
            console.error(err)
        })
};



//####################################################################### Maps 

function getLocation() {
    navigator.geolocation.getCurrentPosition(success, error, options);
}

var options = {
    enableHighAccuracy: true,
    timeout: 5000,
    maximumAge: 0
};

function success(pos) {
    var crd = pos.coords;
    try {
        generateMap(crd.longitude, crd.latitude, 9);
    } catch (error) {
        console.error(error);
    }

    try {
        generateMapEditSubPage(crd.longitude, crd.latitude, 9);
    } catch (error) {
        console.error(error);
    }
   
    /*
    alert(`Latitude : ${crd.latitude}`);
    alert(`Longitude: ${crd.longitude}`);
    */
}

function error(err) {
    console.warn(`ERROR(${err.code}): ${err.message}`);
}





function generateMap(longitude = 19.18945417, latitude = 52.13183904, zoomVal = 6) {
    document.getElementById("displayMap").innerHTML = "";
    document.getElementById("mapContainer").style.display = "block";
    document.getElementById("addActionForm").style.display = "none";
    var map = new ol.Map({
        target: 'displayMap',
        layers: [
            new ol.layer.Tile({
                source: new ol.source.OSM()
            })
        ],
        view: new ol.View({
            center: ol.proj.fromLonLat([longitude, latitude]),
            zoom: zoomVal
        })
    });

    //Stopnie dziesiętne (SD) Szer. geograficzna nDł. geograficzna
    map.on('click', function (evt) {
        var coordinates = (ol.proj.transform(evt.coordinate, 'EPSG:3857', 'EPSG:4326')).toString().split(",");
        document.getElementById("DodajAkcjeLokalizacja").value += coordinates[1] + ",\n" + coordinates[0];
        closeMap();
    });

}


function generateMapEditSubPage(longitude = 19.18945417, latitude = 52.13183904, zoomVal = 6) {
    document.getElementById("displayMapEditSubPage").innerHTML = "";
    document.getElementById("mapContainerEditSubPage").style.display = "block";
    document.getElementById("editActionForm").style.display = "none";
    var map = new ol.Map({
        target: 'displayMapEditSubPage',
        layers: [
            new ol.layer.Tile({
                source: new ol.source.OSM()
            })
        ],
        view: new ol.View({
            center: ol.proj.fromLonLat([longitude, latitude]),
            zoom: zoomVal
        })
    });

    //Stopnie dziesiętne (SD) Szer. geograficzna nDł. geograficzna
    map.on('click', function (evt) {
        var coordinates = (ol.proj.transform(evt.coordinate, 'EPSG:3857', 'EPSG:4326')).toString().split(",");
        document.getElementById("EdytujAkcjeLokalizacja").value = coordinates[1] + ",\n" + coordinates[0];
        closeMapEditSubPage()
    });

}

function closeMap() {
    document.getElementById("mapContainer").style.display = "none";
    document.getElementById("addActionForm").style.display = "block";
}
function closeMapEditSubPage() {
    document.getElementById("mapContainerEditSubPage").style.display = "none";
    document.getElementById("editActionForm").style.display = "block";
}



//####################################################################### DocumentsAction

function onloadActionsPrintPreview() {
    window.print();
}

function addMemberListOfActionClick(sender) {
    var table = document.getElementById("addUserForActionListTable");
    var lastRowIndex = table.rows.length - 1;

    for (let i = 0; i < lastRowIndex + 1; i++) {
        table.rows[i].classList.remove("selectedTableSmRow");
        table.rows[i].cells[0].lastElementChild.checked = false;
    }

    var thisCheckbox = table.rows[sender.rowIndex].cells[0].lastElementChild;

    setTimeout(function () {
        thisCheckbox.checked = thisCheckbox.checked ? false : true;
        if (thisCheckbox.checked)
            table.rows[sender.rowIndex].classList.add("selectedTableSmRow")
        else if (table.rows[sender.rowIndex].classList.contains("selectedTableSmRow"))
            table.rows[sender.rowIndex].classList.remove("selectedTableSmRow");
          
    }
    , 150);

}

function printActionListBtnClick() {
   
    var table = document.getElementById("addUserForActionListTable");
    var lastRowIndex = table.rows.length - 1;
    var selectedUserId = "";
    var preparedDateFrom = "";
    var preparedDateTo = "";
    for (let i = 0; i < lastRowIndex + 1; i++) {
        if (table.rows[i].cells[0].lastElementChild.checked == true) {
            selectedUserId = table.rows[i].cells[1].lastElementChild.value;
        }
    }

    var title = document.getElementById("printActionsListTitle").value;
    var dateFrom = document.getElementById("printActionsListDateFrom").value;
    var dateTo = document.getElementById("printActionsListDateTo").value;

    /*
    if (dateFrom != "") {
        var rawDate = new Date(dateFrom);
        var MonthNumberOnly = rawDate.getMonth() + 1;
        MonthNumberOnly = MonthNumberOnly < 10 ? "0" + MonthNumberOnly : MonthNumberOnly;
        var yearOnly = rawDate.getFullYear();
        var dayOnly = rawDate.getDate();
        dayOnly = dayOnly < 10 ? "0" + dayOnly : dayOnly;
        preparedDateFrom = dayOnly + "." + MonthNumberOnly + "." + yearOnly;
    }
    
    if (dateTo != "") {
        rawDate = new Date(dateTo);
        MonthNumberOnly = rawDate.getMonth() + 1;
        MonthNumberOnly = MonthNumberOnly < 10 ? "0" + MonthNumberOnly : MonthNumberOnly;
        yearOnly = rawDate.getFullYear();
        dayOnly = rawDate.getDate();
        dayOnly = dayOnly < 10 ? "0" + dayOnly : dayOnly;
        preparedDateTo = dayOnly + "." + MonthNumberOnly + "." + yearOnly;
    }
   */

    preparedDateFrom = dateFrom != "" ? dateFrom : "";
    preparedDateTo = dateTo != "" ? dateTo : "";

    var newLocation = location.toString()
    newLocation = newLocation.substring(0, newLocation.indexOf('Documents'));
    newLocation += `Documents/ActionsPrintPreview?Title=
    ${title}&PreparedDateFrom=${preparedDateFrom}&PreparedDateTo=
    ${preparedDateTo}&SelectedUserId=${selectedUserId}`;
    window.open(newLocation, '_blank');
}



//####################################################################### DocumentsTransactions

function onloadTransactionsPrintPreview() {
    window.print();
}

function printTransactionsListBtnClick() {
    var preparedDateFrom = "";
    var preparedDateTo = "";
    var title = document.getElementById("printTransactionsListTitle").value;
    var dateFrom = document.getElementById("printTransactionsListDateFrom").value;
    var dateTo = document.getElementById("printTransactionsListDateTo").value;
    var inflow = document.getElementById("printTransactionsCheckBoxInflow").checked
    var outflow = document.getElementById("printTransactionsCheckBoxOutflow").checked
    var adjustmentPlus = document.getElementById("printTransactionsCheckBoxAdjustmentPlus").checked
    var adjustmentMinus = document.getElementById("printTransactionsCheckBoxAdjustmentMinus").checked
    

    preparedDateFrom = dateFrom != "" ? dateFrom : "";
    preparedDateTo = dateTo != "" ? dateTo : "";

    var newLocation = location.toString()
    newLocation = newLocation.substring(0, newLocation.indexOf('Documents'));
    newLocation += `Documents/TransactionsPrintPreview?Title=
    ${title}&PreparedDateFrom=${preparedDateFrom}&PreparedDateTo=
    ${preparedDateTo}&inflow=${inflow}&outflow=${outflow}
    &adjustmentPlus=${adjustmentPlus}&adjustmentMinus=${adjustmentMinus}`;
    window.open(newLocation, '_blank');
}




function closeReadOnlyInfo(sender) {
    //sessionStorage.setItem("displayReadOnlyInfo", "false");
    if (sessionStorage.getItem("displayReadOnlyInfo") != "false") {
        document.getElementById("readOnlyInfo").style.display = "block";
    }
    if (sender) {
        sender.parentElement.style.display = "none"
        sessionStorage.setItem("displayReadOnlyInfo", "false");
    }

}
