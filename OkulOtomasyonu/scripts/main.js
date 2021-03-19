


function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode > 31 && (charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122) && (charCode != 231 && charCode != 199 && charCode != 199 && charCode != 246 && charCode != 214 && charCode != 304 && charCode != 305 && charCode != 350 && charCode != 351 && charCode != 286 && charCode != 287 && charCode != 220 && charCode != 252))
        return false;
    return true;
}
function isTextKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

function tckimlikkontrolu(tcno) {
    var tckontrol, toplam; tckontrol = tcno; tcno = tcno.value; toplam = Number(tcno.substring(0, 1)) + Number(tcno.substring(1, 2)) +
	Number(tcno.substring(2, 3)) + Number(tcno.substring(3, 4)) +
	Number(tcno.substring(4, 5)) + Number(tcno.substring(5, 6)) +
	Number(tcno.substring(6, 7)) + Number(tcno.substring(7, 8)) +
	Number(tcno.substring(8, 9)) + Number(tcno.substring(9, 10));
    strtoplam = String(toplam); onunbirlerbas = strtoplam.substring(strtoplam.length, strtoplam.length - 1);
    
    if (onunbirlerbas == tcno.substring(10, 11) ) {
       
        $("#tc_noNotError").html("TC Kimlik Numarası geçerli.");
        $("#tc_noError").hide();
        $("#tc_noNotError2").hide();
        $("#tc_noNotError").show();
        document.getElementById("Create").disabled = false;
        document.getElementById("Create").style.cursor = "";
    }
     
    else {
        var deger = document.getElementById("tc_no").value;
        if (deger != null) {
            $("#tc_noError").html("TC Kimlik Numarası geçersiz");
            $("#tc_noNotError").hide();
            $("#tc_noNotError2").hide();
            $("#tc_noError").show();
            document.getElementById("Create").style.cursor = "not-allowed";
            document.getElementById("Create").disabled = true;
        }
    }
    if (Number(tcno.substring(0, 1)) == 0) {
        var deger = document.getElementById("tc_no").value;
        if (deger != null) {
            $("#tc_noError").html("TC Kimlik Numarası geçersiz");
            $("#tc_noNotError").hide();
            $("#tc_noNotError2").hide();
            $("#tc_noError").show();
            document.getElementById("Create").style.cursor = "not-allowed";
            document.getElementById("Create").disabled = true;
        }
    }
}
function spanAdi()
{
    $("#spanadi").hide();
   
}
function spanSoyadi() {
    $("#spansoyadi").hide();

}
function spanTcno() {
    $("#spantcno").hide();

}
function spanOno() {
    $("#spanono").hide();

}
function spanDtarihi() {
    $("#spandtarihi").hide();

}
function spanAdres() {
    $("#spanadres").hide();

}
function spanSifreTekrar() {
    $("#spansifretekrar").hide();

}

/*Öğrenci Ekleme*/
function bootboxCreate() {
   

   
    var adi = document.getElementById("adi").value;
    var soyadi = document.getElementById("soyadi").value;
    var tc_no = document.getElementById("tc_no").value;
    var o_no = document.getElementById("o_no").value;
    var d_tarihi = document.getElementById("d_tarihi").value;
    var adres = document.getElementById("adres").value;
   
        if (adi == "" || soyadi == "" || tc_no == "" || o_no == "" || d_tarihi == "" || adres == "") {

            document.getElementById("myForm").submit();
            
        }
        else {


            bootbox.confirm({
                    message: "Yeni öğrenci eklensin mi?",
                    buttons: {
                    confirm: {
                        label: 'Evet',
                        className: 'btn-success'
                    },
                    cancel: {
                        label: 'Hayır',
                        className: 'btn-danger'
                    }
                },
                callback: function (result) {
                    if (result) {

                        document.getElementById("myForm").submit();
                        
                        return result;
                    }
                    else {
                       
                        return !result;
                    }
                }
            });
        }
}
/*Öğrenci Güncelleme*/

function bootboxUpdate() {



    var adi = document.getElementById("adi").value;
    var soyadi = document.getElementById("soyadi").value;
    var tc_no = document.getElementById("tc_no").value;
    var o_no = document.getElementById("o_no").value;
    var d_tarihi = document.getElementById("d_tarihi").value;
    var adres = document.getElementById("adres").value;

    if (adi == "" || soyadi == "" || tc_no == "" || o_no == "" || d_tarihi == "" || adres == "") {

        document.getElementById("myForm").submit();

    }
    else {


        bootbox.confirm({
            message: "Öğrenci güncellensin mi?",
            buttons: {
                confirm: {
                    label: 'Evet',
                    className: 'btn-success'
                },
                cancel: {
                    label: 'Hayır',
                    className: 'btn-danger'
                }
            },
            callback: function (result) {
                if (result) {

                    document.getElementById("myForm").submit();

                    return result;
                }
                else {

                    return !result;
                }
            }
        });
    }
}
/*Öğrenci Silme*/
function bootboxDelete() {

    bootbox.confirm({
        message: "Öğrenci silinsin mi?",
        buttons: {
            confirm: {
                label: 'Evet',
                className: 'btn-success'
            },
            cancel: {
                label: 'Hayır',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            if (result) {

                document.getElementById("myForm").submit();

                return result;
            }
            else {

                return !result;
            }
        }
    });
}
/*Öğrenciye Ders Atama*/


function bootboxAssignment() {

    bootbox.confirm({
        message: "Öğrenciye ders atansın mı?",
        buttons: {
            confirm: {
                label: 'Evet',
                className: 'btn-success'
            },
            cancel: {
                label: 'Hayır',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            if (result) {

                document.getElementById("myForm").submit();

                return result;
            }
            else {

                return !result;
            }
        }
    });
}


/*Yönetici Ekleme*/
function bootboxCreateAdmin() {



    var k_adi = document.getElementById("k_adi").value;
    var sifre = document.getElementById("sifre").value;
    var email = document.getElementById("email").value;
    var gizlisoru = document.getElementById("gizlisoru").value;
    var gizlicevap = document.getElementById("gizlicevap").value;
    var songiristarihi = document.getElementById("songiristarihi").value;

    if (k_adi == "" || sifre == "" || email == "" || gizlisoru == "" || gizlicevap == "" || songiristarihi == "") {

        document.getElementById("myForm").submit();

    }
    else {


        bootbox.confirm({
            message: "Yeni yönetici eklensin mi?",
            buttons: {
                confirm: {
                    label: 'Evet',
                    className: 'btn-success'
                },
                cancel: {
                    label: 'Hayır',
                    className: 'btn-danger'
                }
            },
            callback: function (result) {
                if (result) {

                    document.getElementById("myForm").submit();

                    return result;
                }
                else {

                    return !result;
                }
            }
        });
    }
}

/*Yönetici Güncelleme*/

function bootboxUpdateAdmin() {



    var k_adi = document.getElementById("k_adi").value;
    var sifre = document.getElementById("sifre").value;
    var email = document.getElementById("email").value;
    var gizlisoru = document.getElementById("gizlisoru").value;
    var gizlicevap = document.getElementById("gizlicevap").value;
    var songiristarihi = document.getElementById("songiristarihi").value;

    if (k_adi == "" || sifre == "" || email == "" || gizlisoru == "" || gizlicevap == "" || songiristarihi == "") {

        document.getElementById("myForm").submit();

    }
    else {


        bootbox.confirm({
            message: "Yönetici güncellensin mi?",
            buttons: {
                confirm: {
                    label: 'Evet',
                    className: 'btn-success'
                },
                cancel: {
                    label: 'Hayır',
                    className: 'btn-danger'
                }
            },
            callback: function (result) {
                if (result) {

                    document.getElementById("myForm").submit();

                    return result;
                }
                else {

                    return !result;
                }
            }
        });
    }
}
/*PASSWORD CHECKBOX*/
function myFunction() {
    var x = document.getElementById("sifre");
    if (x.type === "password") {
        x.type = "text";
    } else {
        x.type = "password";
    }
}
function myFunction2() {
    var x = document.getElementById("sifre2");
    if (x.type === "password") {
        x.type = "text";
    } else {
        x.type = "password";
    }
}
function myFunction3() {
    var x = document.getElementById("sifre");
    if (x.type === "password") {
        x.type = "text";
    } else {
        x.type = "password";
    }
}
/*Admin Silme BootBox*/
function bootboxDeleteAdmin() {

    bootbox.confirm({
        message: "Yönetici silinsin mi?",
        buttons: {
            confirm: {
                label: 'Evet',
                className: 'btn-success'
            },
            cancel: {
                label: 'Hayır',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            if (result) {

                document.getElementById("myForm").submit();

                return result;
            }
            else {

                return !result;
            }
        }
    });
}
/*Ders Ekleme*/
function bootboxCreateLesson() {



    var ders_no = document.getElementById("ders_no").value;
    var ders_adi = document.getElementById("ders_adi").value;
    var ders_hocasi = document.getElementById("ders_hocasi").value;
   

    if (ders_no == "" || ders_adi == "" || ders_hocasi == "" ) {

        document.getElementById("myForm").submit();

    }
    else {


        bootbox.confirm({
            message: "Yeni ders eklensin mi?",
            buttons: {
                confirm: {
                    label: 'Evet',
                    className: 'btn-success'
                },
                cancel: {
                    label: 'Hayır',
                    className: 'btn-danger'
                }
            },
            callback: function (result) {
                if (result) {

                    document.getElementById("myForm").submit();

                    return result;
                }
                else {

                    return !result;
                }
            }
        });
    }
}

/*Ders Güncelleme*/
function bootboxUpdateLesson() {



    var ders_no = document.getElementById("ders_no").value;
    var ders_adi = document.getElementById("ders_adi").value;
    var ders_hocasi = document.getElementById("ders_hocasi").value;


    if (ders_no == "" || ders_adi == "" || ders_hocasi == "") {

        document.getElementById("myForm").submit();

    }
    else {


        bootbox.confirm({
            message: "Ders güncellensin mi?",
            buttons: {
                confirm: {
                    label: 'Evet',
                    className: 'btn-success'
                },
                cancel: {
                    label: 'Hayır',
                    className: 'btn-danger'
                }
            },
            callback: function (result) {
                if (result) {

                    document.getElementById("myForm").submit();

                    return result;
                }
                else {

                    return !result;
                }
            }
        });
    }
}
function bootboxDeleteLesson() {

    bootbox.confirm({
        message: "Ders silinsin mi?",
        buttons: {
            confirm: {
                label: 'Evet',
                className: 'btn-success'
            },
            cancel: {
                label: 'Hayır',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            if (result) {

                document.getElementById("myForm").submit();

                return result;
            }
            else {

                return !result;
            }
        }
    });
}