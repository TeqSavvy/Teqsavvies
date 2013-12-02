
$(document).ready(function() {


    var addPatient = function(patient) {

        $.getJSON("/Clinic/AddPatient", patient, function(result) {

            alert(result);
        });
    };
});