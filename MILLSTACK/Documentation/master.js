
$(document).ready(function () {



    //  -----------------------------------------------------------------------------
    //  |                       Select2 Dropdown Plugin                             |
    //  -----------------------------------------------------------------------------
    $('#main_XXXXXX').select2({
        theme: 'classic',
        placeholder: 'Select here.....',
        allowClear: false,
    });




    //  -----------------------------------------------------------------------------
    //  |                       Chosen Dropdown Plugin                              |
    //  -----------------------------------------------------------------------------

    function Bind_Dropdown_By_ID(dropdownID) {
        $(dropdownID).chosen({
            disable_search: false, // disable search bar
            search_contains: false, // search characters anywhere instead of right-left
            no_results_text: "No results match", // if searched not matched
            placeholder_text_single: "Select Here......", // search bar place holder 
            allow_single_deselect: true, // clear selected item
            width: "100%", // width of element
            rtl: false, // text appears on right side right-to-left
        });
    }

    Bind_Dropdown_By_ID("#ddSc_Batch_no");





    //  -----------------------------------------------------------------------------
    //  |        Sumo Select (Multi-Select or Multi-Check) Dropdown Plugin          |
    //  -----------------------------------------------------------------------------

    function Bind_Dropdown_Multicheck_By_ID(dropdownID) {
        $(dropdownID).on('sumo:opening', function () {
            $('.select-all').css('height', '40px');
        });

        $(dropdownID).SumoSelect({
            search: true,
            searchText: "search here....",
            multiSelect: true,
            okCancelInMulti: true,
            prefix: "",
            up: false, // list at top side
            selectAll: true, // select all option
            clearAll: true, // not working at all
            renderOption: function (data, escape) {
                // Check if the option is the "Select Values" item
                if (data.value === "0") {
                    // Add a class to "Select Values" item to handle differently
                    return '<div class="title special-item">' + escape(data.text) + '</div>';
                } else {
                    return '<div><label><input type="checkbox" />' + escape(data.text) + '</label></div>';
                }
            }
        });
        $(dropdownID).on('sumo:opened', function () {
            // Check if the "Select Values" item is present
            if ($(dropdownID + ' .special-item').length > 0) {
                // Disable multiselect for the "Select Values" item
                $(dropdownID + ' .special-item input').prop('disabled', true);
            }
        });
    }

    Bind_Dropdown_Multicheck_By_ID("#MCDD_Bill_No");




    //  -----------------------------------------------------------------------------
    //  |                       Datatables.js UI Plugin                             |
    //  -----------------------------------------------------------------------------
    function Initialize_DataTables_By_ID(GridView_ID) {
        $(GridView_ID).each(function () {
            var $this = $(this);
            $this.prepend(
                $("<thead></thead>").append(
                    $this.find("tr:first")
                )
            ).DataTable({
                scrollX: false,
                sScrollXInner: "100%",
                bFilter: true,
                bSort: true,
                bPaginate: true,
                scrollCollapse: false,
                paging: true,
                searching: true,
                ordering: true,
                info: true,
                lengthChange: true,
                responsive: true,
                pagingType: 'full_numbers',
                lengthMenu: [
                    [10, 20, 25, 50, -1],
                    [10, 20, 25, 50, "All"]
                ],
                search: {
                    return: false
                },
                language: {
                    search: "Search: ",
                    decimal: ',',
                    thousands: '.'
                },
                initComplete: function () {
                    $('.dataTables_filter input').attr('placeholder', 'Search here......');
                },
                dom: '<"top"lfB>rt<"bottom"ip><"clear">', // Configure DOM for buttons and entries (initial value - Bfrtip)
                buttons: [
                    //'copy', 'csv', 'excel', 'pdf', 'print' // Add export buttons
                    {
                        extend: 'copy',
                        title: 'Customer Data',
                        filename: 'Customer_Data_Copy'
                    },
                    {
                        extend: 'csv',
                        title: 'Customer Data',
                        filename: 'Customer Data'
                    },
                    {
                        extend: 'excel',
                        title: 'Customer Data',
                        filename: 'Customer Data'
                    },
                    {
                        extend: 'pdf',
                        title: 'Customer Data',
                        filename: 'Customer Data'
                    },
                    {
                        extend: 'print',
                        title: 'Customer Data'
                    }
                ]
            });
        });
    }


    Initialize_DataTables_By_ID("#Grid_Search");








    // Reinitialize Select2 after UpdatePanel partial postback
    var prm = Sys.WebForms.PageRequestManager.getInstance();

    // Reinitialize Select2 for all dropdowns
    prm.add_endRequest(function () {

        setTimeout(function () {



        }, 0);
    });

});