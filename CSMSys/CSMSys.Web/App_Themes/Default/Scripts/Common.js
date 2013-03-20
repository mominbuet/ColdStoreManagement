///Java Script to Format Date value to Local (Bengali) equivalent string

FormatDateToLocal = function () {

    debugger;

    var culture = "Culture";
    var siteCultureBN = "bn-IN";
    var siteCultureEN = "en-US";

    var activeCulture = "<%=Session.Contents('BATCHID')%>";


    if (activeCulture == siteCultureBN) {

    }

    FormatDateToLocal = function (dateItem) {

        ///debugger;

        var culture = "Culture";
        var siteCultureBN = "bn-IN";
        var siteCultureEN = "en-US";


        var activeCulture = '<%= Session["Culture"] %>';

        if (activeCulture == siteCultureBN) {
            return GetLocalDateString(dateItem);
        }
        else {
            return GetDefaultDateString(dateItem);
        }
    }

    GetDateLookUp = function () {

        var arrayLookup = { '1': '১', '2': '২',
            '3': '৩', '4': '৪',
            '5': '৫', '6': '৬',
            '7': '৭', '8': '৮',
            '9': '৯', '0': '০'
        }

        return arrayLookup;
    }

    GetDefaultDateString = function (dateValue) {
        dateValue = new Date(dateValue);

        var dt;

        dt = dateValue.getFullYear();
        dt += '-';
        dt += dateValue.getMonth() + 1;
        dt += '-';
        dt += dateValue.getDate();

        return dt;
    }

    GetLocalDateString = function (dateValue) {
        dateValue = new Date(dateValue);

        var dt;
        var temp;

        dt = GetLocalValue(dateValue.getFullYear());
        dt += '-';
        dt = GetLocalValue(dateValue.getMonth());
        dt += '-';
        dt = GetLocalValue(dateValue.getDate());

        return dt;
    }

    GetLocalValue = function (value) {
        var arrayLookup = GetDateLookUp();
        var dt = '';

        value = value.toString();

        for (var i = 0; i < value.length; i++) {

            dt += arrayLookup[value[i]].toString();
        }
    }

}