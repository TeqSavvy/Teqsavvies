

$(document).ready(function () {

    //create staff
    var staff = { "fullname": "", "username": "", "password": "" , "phonenumber" : "",  "email" : ""};
    var ceateStaff = function() {
        $.getJSON("/Staff/Create", staff, function(result) {

        });
    };
    
    var loginStaff = function (e) {
        e.preventDefault();
        var username = $("#txtUsername").val();
        var password = $("#txtPassword").val();
       
        $.getJSON("/Clinic/Authenticate", "username=" + username + "&password=" + password, function (result) {
            alert(result);
            if (result.Id == null) {
                toastr.info("Username or Password Incorrect");
            }

            if (result.Role == "Admin") {
                toastr.info("Welcome" + result.Fullname);
                
                window.location = "/Clinic/Admin";
                
            }
            else {
                window.location = "/Clinic/Staff";
            }

        });

    };

    //click login button
    $(".loginBtn").click(loginStaff);
    

});