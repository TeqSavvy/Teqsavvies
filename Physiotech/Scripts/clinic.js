$(document).ready(function () {

   
    var placeholder = $(".placeholder");
    placeholder.load("/Staff/Home");
    var create = $(".create_holder");
    var lsPatient = $(".lsPatient");
    var lsHome = $(".lsHome");
    var txtFullname = $("#search_fullname");
    var btnCancel = $("#btnCancel");
    var btnCreatePatient = $("#btnCreatePatient");
    var btnSearch = $("#btnSearch");

    var btnAdd = $(".btn_add_patient");
   

    btnSearch.on("click", function () {
        
        var param = $("#txtPatientId").val();

        placeholder.load("/Clinic/SearchResult");
        $.getJSON("/Clinic/SearchPatient", param, function (result) {

            

            console.log(result.Fullname);
            txtFullname.text(result.Fullname);
                
            });
          //  console.log(result.Fullname);
        });
    

    lsPatient.click(function () {
        // alert("loads all patients");
        lsPatient.css({ color: "orangered" });
        lsHome.css({ color: "#A89E9c" });
        placeholder.empty();

        placeholder.load("/Clinic/AllPatients", function () {
            var patientTable = $(".patientTable");
            $.getJSON("/Clinic/LoadPatients", "", function (callback) {
                for (var i = 0; i < callback.length; i++) {

                    console.log(callback[i].Fullname);
                    patientTable.append("<tr>" +
                    "<td>" +
                    "" + callback[i].Fullname +
                    "</td>" +
                    "<td>" +
                    "" + callback[i].EmailAddress +
                    "<td>" +
                    "" + callback[i].Phonenumber +
                    "</td>" +
                    "</td>" +
                    "</tr>");
                }
                
            });
        }).fadeIn();

       
       // loadPatients();
        //$(document).on('click', "#btnTest", function() {
        //    alert("ehello");
        //});

      
    });

    lsHome.click(function () {
        var controls = $(".control_div li").css({ color: "#A89E9c" });
        lsHome.css({ color: "orangered" });
        placeholder.load("/Staff/Home");

    });

    btnAdd.live("hover", function () {
        btnAdd.css({ background: "red" });
    });

    btnAdd.live("mouseenter", function () {
        btnAdd.css({ background: "red" });
    });

    btnAdd.live("click", function () {
        //  placeholder.fadeOut();
        placeholder.load("/Staff/CreatePatient");
        //create.fadeIn();
    });

    btnCancel.live("click", function () {
        placeholder.load("/Staff/Home");
    });


    btnCreatePatient.live("click", function () {

        var id = $("#patientId").val();
        var fullname = $("#patientFullname").val();
        var nextKinMobile = $("#patientKinMobile").val();
        var email = $("#patientEmail").val();
        var address = $("#patientAddress").val();
        var phone = $("#patientMobile").val();
        var dob = $("#patientDob").val();
        var patient = { "PatientId": id, "Fullname": fullname, "Phonenumber": phone, "Address": address, "Dob": dob, "EmailAddress": email, "NextofKinMobile": nextKinMobile };

        $.getJSON("/Clinic/AddPatient/", patient, function (result) {
            if (result == "created") {
                alert("patient created");
                create.fadeOut();
                placeholder.fadeIn();
            } else {
                alert("Error creating patient");
            }


        });



    });
    var loadPatients = function () {
        var patientTable = $(".patientTable");
        $.getJSON("/Clinic/LoadPatients", "", function (callback) {
            var btnTest = $("#btnTest");
            btnTest.on("click", function () {
               $(this).append("<h1>cscscs</h1>");
            });
        
            for (var i = 0; i < callback.length; i++) {

                console.log(callback[i].Fullname);
              
               
                //patientTable.live({
                //    ready : function () {
                //        $(this).append("<tr>" +
                //       "<td>" +
                //       "" + callback[i].Fullname + 
                //       "</td>" +
                //       "<td>" +
                //       "" + callback[i].EmailAddress +
                //       "<td>" +
                //       "" + callback[i].Phonenumber + 
                //       "</td>" +
                //       "</td>" +
                //       "</tr>");
                //    }
                
                //patientTable.live.append("<tr>" +
                //    "<td>" +
                //    "" + callback[i].Fullname + 
                //    "</td>" +
                //    "<td>" +
                //    "" + callback[i].EmailAddress +
                //    "<td>" +
                //    "" + callback[i].Phonenumber + 
                //    "</td>" +
                //    "</td>" +
                //    "</tr>");
            }
        });
    };
});