export class Table {
    constructor(tableID, data, columnDefs, options, parentClass) {
        const self = this;

        this.tableEL = document.querySelector(`${tableID}`);
        this.data = data;
        this.columnDefs = columnDefs;
        this.options = options;
        this.parentClass = parentClass;
        this.cachedPageLength = this.getCachedPageLength();
        this.initialOrder = this.options.order;

        this.init();
    }

    init() {
        const self = this;
        const cachedPageLength = parseInt(self.cachedPageLength);

        self.dt = $(this.tableEL).DataTable({
            data: self.data ? self.data : '',
            lengthChange: true,
            pageLength: cachedPageLength,
            dom: 'Br<"top"<"d-flex justify-content-between"<li>p>>t<"d-flex justify-content-between"<li>p>',
            autoWidth: false,
            searching: true,
            pagingType: 'full_numbers',
            ordering: false,
            paging: true,
            info: true,
            language: {
                language: {
                    processing: "Loading..."
                },
                paginate: {
                    first: '<i class="fa fa-step-backward"></i>',
                    previous: '<i class="fa fa-backward"></i>',
                    next: '<i class="fa fa-forward"></i>',
                    last: '<i class="fa fa-step-forward"></i>'
                }
            },
            columnDefs: self.columnDefs,
            ...self.options,
        });
    }

    getCachedPageLength() {
        return  10;
    }

    getTotalRecordsCount() {
        const table = $(".dataTable:visible").DataTable();

        return table.page.info().recordsDisplay;
    }

    getRequestData() {
        const table = $(".dataTable:visible").DataTable();
        const columnInfo = table.columns().header().toArray().map(header => $(header).html());

        if (table.settings()[0].ajax) {
            const tableParams = table.ajax.params();

            var columnList = [];

            tableParams.columns.forEach(function (item) {
                var newElem = {
                    Data: item.data,
                    Name: item.name,
                    Orderable: item.orderable
                };

                if (newElem.Name !== '') {
                    columnList.push(newElem);
                }

            });

            let request = {
                Draw: parseInt(tableParams.draw),
                Columns: columnList,
                Order: tableParams.order,
                Start: parseInt(tableParams.start),
                Length: parseInt(tableParams.length)
            };

            return request;
        } else {
            const tableState = table.state();
            const columnDefs = table.settings()[0].aoColumns;

            let columnList = [];

            columnDefs.forEach(function (column, index) {
                let newElem = {
                    Data: column.mData,
                    Name: column.sName,
                    Orderable: column.bSortable
                };

                if (newElem.Name !== '') {
                    columnList.push(newElem);
                }

            });

            let orderList = tableState.order.map(function (item) {
                return {
                    Column: item[0],
                    Dir: item[1]
                };
            });

            let request = {
                Draw: table.settings()[0].iDraw,
                Columns: columnList,
                Order: orderList,
                Start: tableState.start,
                Length: tableState.length,
            };

            return request;
        }
    }

    getRow(tr) {
        return $(this.tableEL).DataTable().row(tr);
    }

    getRows() {
        return $(this.tableEL).DataTable().rows();
    }

    selectAllVisibleRows() {
        $(this.tableEL).dataTable().rows().select();
    }

    deselectAllVisibleRows() {
        $(this.tableEL).dataTable().rows().deselect();
    }

    getSelectedRecordsCount() {
        return $(this.tableEL).DataTable().rows({ selected: true }).count();
    }

    getSelectedRecords() {
        return $(this.tableEL).DataTable().rows({ selected: true }).data();
    }

    getVisibleData() {

        return $(this.tableEL).DataTable().rows({ page: 'current' }).data();
    }

    reload() {
        $(this.tableEL).DataTable().ajax.reload(function () { }, false);
    }

    destroy() {
        $(this.tableEL).DataTable().destroy();
    }
}