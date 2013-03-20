// add parser through the tablesorter addParser method
$.tablesorter.addParser(
{
    // set a unique id 
    id: 'formattedNumbers',
    is: function(s) 
    {
        // return false so this parser is not auto detected 
        return false;
    },
    format: function(s) 
    {
        // format your data for normalization
       
        return s.replace(/,/g, ''); //removes comma separator from formatted numbers
    },
    // set type, either numeric or text 
    type: 'numeric'
});

function pageLoad(sender, args) 
{
    var columnIndex = parseInt($get("hfInitialSortByColumnIndex").value);       
        
    if ($get("hfIsDebug").value == "false") 
    {
        $("#grvDistributor").tablesorter(
        {
            sortList: [[columnIndex, 1]],
            dateFormat: "uk", //sets the date format to dd/MM/yyyy
            headers:
            {
                6: { sorter: 'formattedNumbers' },
                8: { sorter: 'formattedNumbers' }
            },
            textExtraction: extractValue
        });
    }
    else
    {
        $("#grvDistributor").tablesorter(
        {
            debug: true, //enables debug mode
            sortList: [[columnIndex, 1]],
            dateFormat: "uk", //sets the date format to dd/MM/yyyy
            headers:
            {
                6: { sorter: 'formattedNumbers' },
                8: { sorter: 'formattedNumbers' }
            },
            textExtraction: extractValue
        });
    }
}

function extractValue(node)
{    
    var children = node.childNodes[0].childNodes.length
   
    if (children == 0) //boundTextField or  a templateColumn
    {
        if (node.childNodes[0].nodeType == 3)//boundTextField
        {
            return node.childNodes[0].data;
        }
        else //template column
        {
            var type = node.childNodes[0].type
            
            switch (type) 
            {
                case "checkbox":
                    return node.childNodes[0].checked.toString()

                case "radio":
                    return node.childNodes[0].checked.toString()

                case "text":
                    return node.childNodes[0].value

                default: return ""
            }
        }
    }
    else //boundCheckboxColumn or a templateLabelColumn
    {
        if (node.childNodes[0].childNodes[0].nodeType == 3)
        {
            return node.childNodes[0].childNodes[0].data;  
        }
        else
        {
            return node.childNodes[0].childNodes[0].checked.toString();
        }
    }
}