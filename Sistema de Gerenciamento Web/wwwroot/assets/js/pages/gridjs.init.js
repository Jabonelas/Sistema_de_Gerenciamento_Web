/*
Template Name: Velzon - Admin & Dashboard Template
Author: Themesbrand
Website: https://Themesbrand.com/
Contact: Themesbrand@gmail.com
File: grid Js File
*/

//Produtos Cadastrados
if (document.getElementById("table-gridjs")) {
    $.ajax({
        //url: "Produto/BuscarTodosProdutosCadastrados",
        url: "BuscarTodosProdutosCadastrados",
        method: "GET",
        success: function (data) {
            const arrayPrincipal = [];

            for (var i = 0; i < data.length; i++) {
                arrayPrincipal.push([
                    data[i].id_produto, data[i].pd_nome, data[i].pd_codigo, data[i].pd_codigo_barras, "R$ " + data[i].pd_custo, data[i].pd_margem + "%", "R$ " + data[i].pd_preco
                ]);
            }

            // Basic Table
            if (document.getElementById("table-gridjs"))
                new gridjs.Grid({
                    columns: [{
                        name: 'ID',
                        width: '50px',
                        formatter: (function (cell) {
                            return gridjs.html('<span class="fw-semibold">' + cell + '</span>');
                        })
                    },
                    {
                        name: 'Nome',
                        width: '400px',
                    },
                    {
                        name: 'Codigo Produto',
                        width: '150px',
                        //formatter: (function (cell) {
                        //    return gridjs.html('<a href="">' + cell + '</a>');
                        //})
                    },
                    {
                        name: 'Codigo Barras',
                        width: '150px',
                    },
                    {
                        name: 'Custo',
                        width: '100px',
                    },
                    {
                        name: 'Margem Lucro',
                        width: '130px',
                    },
                    {
                        name: 'Preco',
                        width: '150px',
                        //formatter: (function (cell) {
                        //    return gridjs.html("<a href='#' class='text-reset text-decoration-underline'>" +
                        //        "Details" +
                        //        "</a>");
                        //})
                    },
                    ],
                    pagination: {
                        limit: 5
                    },
                    sort: true,
                    search: true,
                    language: {
                        search: {
                            placeholder: 'Buscar...'
                        },

                        pagination: {
                            previous: "Anterior", next: "Proximo",

                            showing: "Exibindo", of: "de", to: "a", results: "Resultados"
                        }
                    },
                    data: arrayPrincipal
                }).render(document.getElementById("table-gridjs"));
        },
        error: function (error) {
            console.error(error);
        }
    });
};

if (document.getElementById("table-gridjs1")) {
    $.ajax({
        //url: "Produto/BuscarEstoqueProdutos",
        url: "BuscarEstoqueProdutos",
        method: "GET",
        success: function (data) {
            const arrayPrincipal = [];

            for (var i = 0; i < data.length; i++) {
                arrayPrincipal.push([
                    data[i].id_produto, data[i].pd_nome, data[i].pd_codigo, data[i].ep_codigo_barras, data[i].ep_status_situacao,
                    data[i].ep_quantidade, data[i].pd_tipo_unidade, data[i].pd_estoque_minimo

                ]);
            }

            // Basic Table
            if (document.getElementById("table-gridjs1"))
                new gridjs.Grid({
                    columns: [{
                        name: 'ID',
                        width: '50px',
                        formatter: (function (cell) {
                            return gridjs.html('<span class="fw-semibold">' + cell + '</span>');
                        })
                    },
                    {
                        name: 'Nome',
                        width: '400px',
                    },
                    {
                        name: 'Cod. Produto',
                        width: '130px',
                        //formatter: (function (cell) {
                        //    return gridjs.html('<a href="">' + cell + '</a>');
                        //})
                    },
                    {
                        name: 'Cod. Barras',
                        width: '130px',
                    },
                    {
                        name: 'Status',
                        width: '80px',
                    },
                    {
                        name: 'Qtd.',
                        width: '80px',
                    },
                    {
                        name: 'Und.',
                        width: '80px',
                    }, {
                        name: 'Est. Minimo',
                        width: '100px',
                    },
                    ],
                    pagination: {
                        limit: 5
                    },
                    sort: true,
                    search: true,
                    language: {
                        search: {
                            placeholder: 'Buscar...'
                        },

                        pagination: {
                            previous: "Anterior", next: "Proximo",

                            showing: "Exibindo", of: "de", to: "a", results: "Resultados"
                        }
                    },
                    data: arrayPrincipal
                }).render(document.getElementById("table-gridjs1"));
        },
        error: function (error) {
            console.error(error);
        }
    });
};

////Consultar Estoque
//if (document.getElementById("table-gridjs-Estoque-Produto")) {
//    $.ajax({
//                url: "Produto/BuscarEstoqueProdutos",
//        //url: "Produto/BuscarEstoqueProdutos",
//        method: "GET",
//        success: function (data) {
//            const arrayPrincipal = [];

//            for (var i = 0; i < data.length; i++) {
//                arrayPrincipal.push([
//                    data[i].id_produto, data[i].pd_nome, data[i].pd_codigo, data[i].ep_codigo_barras, "R$ " + data[i].pd_custo, data[i].pd_margem + "%", "R$ " + data[i].pd_preco
//                    , data[i].ep_quantidade, data[i].pd_tipo_unidade, data[i].pd_estoque_minimo

//                ]);
//            }

//            console.log(arrayPrincipal)

//            // Basic Table
//            if (document.getElementById("table-gridjs-Estoque-Produto"))
//                new gridjs.Grid({
//                    columns: [{
//                        name: 'ID',
//                        width: '80px',
//                        formatter: (function (cell) {
//                            return gridjs.html('<span class="fw-semibold">' + cell + '</span>');
//                        })
//                    },
//                    {
//                        name: 'Nome',
//                        width: '400px',
//                    },
//                    {
//                        name: 'Codigo Produto',
//                        width: '150px',
//                        //formatter: (function (cell) {
//                        //    return gridjs.html('<a href="">' + cell + '</a>');
//                        //})
//                    },
//                    {
//                        name: 'Codigo Barras',
//                        width: '150px',
//                    },
//                    {
//                        name: 'Custo Produto',
//                        width: '150px',
//                    },
//                    {
//                        name: 'Margem Lucro',
//                        width: '150px',
//                    },
//                    {
//                        name: 'Preco',
//                        width: '150px',
//                        //formatter: (function (cell) {
//                        //    return gridjs.html("<a href='#' class='text-reset text-decoration-underline'>" +
//                        //        "Details" +
//                        //        "</a>");
//                        //})
//                    },
//                    ],
//                    pagination: {
//                        limit: 5
//                    },
//                    sort: true,
//                    search: true,
//                    language: {
//                        search: {
//                            placeholder: 'Buscar...'
//                        },

//                        pagination: {
//                            previous: "Anterior", next: "Proximo",

//                            showing: "Exibindo", of: "de", to: "a", results: "Resultados"
//                        }
//                    },
//                    data: arrayPrincipal
//                }).render(document.getElementById("table-gridjs-Estoque-Produto"));
//        },
//        error: function (error) {
//            console.error(error);
//        }
//    });
//};

// card Table
if (document.getElementById("table-card"))
    new gridjs.Grid({
        columns: [{
            name: 'Name',
            width: '150px',
        }, {
            name: 'Email',
            width: '250px',
        }, {
            name: 'Position',
            width: '250px',
        }, {
            name: 'Company',
            width: '250px',
        }, {
            name: 'Country',
            width: '150px',
        }],
        sort: true,
        pagination: {
            limit: 5
        },
        data: [
            ["Jonathan", "jonathan@example.com", "Senior Implementation Architect", "Hauck Inc", "Holy See"],
            ["Harold", "harold@example.com", "Forward Creative Coordinator", "Metz Inc", "Iran"],
            ["Shannon", "shannon@example.com", "Legacy Functionality Associate", "Zemlak Group", "South Georgia"],
            ["Robert", "robert@example.com", "Product Accounts Technician", "Hoeger", "San Marino"],
            ["Noel", "noel@example.com", "Customer Data Director", "Howell - Rippin", "Germany"],
            ["Traci", "traci@example.com", "Corporate Identity Director", "Koelpin - Goldner", "Vanuatu"],
            ["Kerry", "kerry@example.com", "Lead Applications Associate", "Feeney, Langworth and Tremblay", "Niger"],
            ["Patsy", "patsy@example.com", "Dynamic Assurance Director", "Streich Group", "Niue"],
            ["Cathy", "cathy@example.com", "Customer Data Director", "Ebert, Schamberger and Johnston", "Mexico"],
            ["Tyrone", "tyrone@example.com", "Senior Response Liaison", "Raynor, Rolfson and Daugherty", "Qatar"],
        ]
    }).render(document.getElementById("table-card"));

// pagination Table
if (document.getElementById("table-pagination"))
    new gridjs.Grid({
        columns: [{
            name: 'ID',
            width: '120px',
            formatter: (function (cell) {
                return gridjs.html('<a href="" class="fw-medium">' + cell + '</a>');
            })
        }, {
            name: 'Name',
            width: '150px',
        }, {
            name: 'Date',
            width: '180px',
        }, {
            name: 'Total',
            width: '120px',
        }, {
            name: 'Status',
            width: '120px',
        },
        {
            name: 'Actions',
            width: '100px',
            formatter: (function (cell) {
                return gridjs.html("<button type='button' class='btn btn-sm btn-light'>" +
                    "Details" +
                    "</button>");
            })
        },
        ],
        pagination: {
            limit: 5
        },

        data: [
            ["#VL2111", "Jonathan", "07 Oct, 2021", "$24.05", "Paid",],
            ["#VL2110", "Harold", "07 Oct, 2021", "$26.15", "Paid"],
            ["#VL2109", "Shannon", "06 Oct, 2021", "$21.25", "Refund"],
            ["#VL2108", "Robert", "05 Oct, 2021", "$25.03", "Paid"],
            ["#VL2107", "Noel", "05 Oct, 2021", "$22.61", "Paid"],
            ["#VL2106", "Traci", "04 Oct, 2021", "$24.05", "Paid"],
            ["#VL2105", "Kerry", "04 Oct, 2021", "$26.15", "Paid"],
            ["#VL2104", "Patsy", "04 Oct, 2021", "$21.25", "Refund"],
            ["#VL2103", "Cathy", "03 Oct, 2021", "$22.61", "Paid"],
            ["#VL2102", "Tyrone", "03 Oct, 2021", "$25.03", "Paid"],
        ]
    }).render(document.getElementById("table-pagination"));

// search Table
if (document.getElementById("table-search"))
    new gridjs.Grid({
        columns: [{
            name: 'Name',
            width: '150px',
        }, {
            name: 'Email',
            width: '250px',
        }, {
            name: 'Position',
            width: '250px',
        }, {
            name: 'Company',
            width: '250px',
        }, {
            name: 'Country',
            width: '150px',
        }],
        pagination: {
            limit: 5
        },
        search: true,
        data: [
            ["Jonathan", "jonathan@example.com", "Senior Implementation Architect", "Hauck Inc", "Holy See"],
            ["Harold", "harold@example.com", "Forward Creative Coordinator", "Metz Inc", "Iran"],
            ["Shannon", "shannon@example.com", "Legacy Functionality Associate", "Zemlak Group", "South Georgia"],
            ["Robert", "robert@example.com", "Product Accounts Technician", "Hoeger", "San Marino"],
            ["Noel", "noel@example.com", "Customer Data Director", "Howell - Rippin", "Germany"],
            ["Traci", "traci@example.com", "Corporate Identity Director", "Koelpin - Goldner", "Vanuatu"],
            ["Kerry", "kerry@example.com", "Lead Applications Associate", "Feeney, Langworth and Tremblay", "Niger"],
            ["Patsy", "patsy@example.com", "Dynamic Assurance Director", "Streich Group", "Niue"],
            ["Cathy", "cathy@example.com", "Customer Data Director", "Ebert, Schamberger and Johnston", "Mexico"],
            ["Tyrone", "tyrone@example.com", "Senior Response Liaison", "Raynor, Rolfson and Daugherty", "Qatar"],
        ]
    }).render(document.getElementById("table-search"));

// Sorting Table
if (document.getElementById("table-sorting"))
    new gridjs.Grid({
        columns: [{
            name: 'Name',
            width: '150px',
        }, {
            name: 'Email',
            width: '250px',
        }, {
            name: 'Position',
            width: '250px',
        }, {
            name: 'Company',
            width: '250px',
        }, {
            name: 'Country',
            width: '150px',
        }],
        pagination: {
            limit: 5
        },
        sort: true,
        data: [
            ["Jonathan", "jonathan@example.com", "Senior Implementation Architect", "Hauck Inc", "Holy See"],
            ["Harold", "harold@example.com", "Forward Creative Coordinator", "Metz Inc", "Iran"],
            ["Shannon", "shannon@example.com", "Legacy Functionality Associate", "Zemlak Group", "South Georgia"],
            ["Robert", "robert@example.com", "Product Accounts Technician", "Hoeger", "San Marino"],
            ["Noel", "noel@example.com", "Customer Data Director", "Howell - Rippin", "Germany"],
            ["Traci", "traci@example.com", "Corporate Identity Director", "Koelpin - Goldner", "Vanuatu"],
            ["Kerry", "kerry@example.com", "Lead Applications Associate", "Feeney, Langworth and Tremblay", "Niger"],
            ["Patsy", "patsy@example.com", "Dynamic Assurance Director", "Streich Group", "Niue"],
            ["Cathy", "cathy@example.com", "Customer Data Director", "Ebert, Schamberger and Johnston", "Mexico"],
            ["Tyrone", "tyrone@example.com", "Senior Response Liaison", "Raynor, Rolfson and Daugherty", "Qatar"],
        ]
    }).render(document.getElementById("table-sorting"));

// Loading State Table
if (document.getElementById("table-loading-state"))
    new gridjs.Grid({
        columns: [{
            name: 'Name',
            width: '150px',
        }, {
            name: 'Email',
            width: '250px',
        }, {
            name: 'Position',
            width: '250px',
        }, {
            name: 'Company',
            width: '250px',
        }, {
            name: 'Country',
            width: '150px',
        }],
        pagination: {
            limit: 5
        },
        sort: true,
        data: function () {
            return new Promise(function (resolve) {
                setTimeout(function () {
                    resolve([
                        ["Jonathan", "jonathan@example.com", "Senior Implementation Architect", "Hauck Inc", "Holy See"],
                        ["Harold", "harold@example.com", "Forward Creative Coordinator", "Metz Inc", "Iran"],
                        ["Shannon", "shannon@example.com", "Legacy Functionality Associate", "Zemlak Group", "South Georgia"],
                        ["Robert", "robert@example.com", "Product Accounts Technician", "Hoeger", "San Marino"],
                        ["Noel", "noel@example.com", "Customer Data Director", "Howell - Rippin", "Germany"],
                        ["Traci", "traci@example.com", "Corporate Identity Director", "Koelpin - Goldner", "Vanuatu"],
                        ["Kerry", "kerry@example.com", "Lead Applications Associate", "Feeney, Langworth and Tremblay", "Niger"],
                        ["Patsy", "patsy@example.com", "Dynamic Assurance Director", "Streich Group", "Niue"],
                        ["Cathy", "cathy@example.com", "Customer Data Director", "Ebert, Schamberger and Johnston", "Mexico"],
                        ["Tyrone", "tyrone@example.com", "Senior Response Liaison", "Raynor, Rolfson and Daugherty", "Qatar"]
                    ])
                }, 2000);
            });
        }
    }).render(document.getElementById("table-loading-state"));

// Fixed Header
if (document.getElementById("table-fixed-header"))
    new gridjs.Grid({
        columns: [{
            name: 'Name',
            width: '150px',
        }, {
            name: 'Email',
            width: '250px',
        }, {
            name: 'Position',
            width: '250px',
        }, {
            name: 'Company',
            width: '250px',
        }, {
            name: 'Country',
            width: '150px',
        }],
        sort: true,
        pagination: true,
        fixedHeader: true,
        height: '400px',
        data: [
            ["Jonathan", "jonathan@example.com", "Senior Implementation Architect", "Hauck Inc", "Holy See"],
            ["Harold", "harold@example.com", "Forward Creative Coordinator", "Metz Inc", "Iran"],
            ["Shannon", "shannon@example.com", "Legacy Functionality Associate", "Zemlak Group", "South Georgia"],
            ["Robert", "robert@example.com", "Product Accounts Technician", "Hoeger", "San Marino"],
            ["Noel", "noel@example.com", "Customer Data Director", "Howell - Rippin", "Germany"],
            ["Traci", "traci@example.com", "Corporate Identity Director", "Koelpin - Goldner", "Vanuatu"],
            ["Kerry", "kerry@example.com", "Lead Applications Associate", "Feeney, Langworth and Tremblay", "Niger"],
            ["Patsy", "patsy@example.com", "Dynamic Assurance Director", "Streich Group", "Niue"],
            ["Cathy", "cathy@example.com", "Customer Data Director", "Ebert, Schamberger and Johnston", "Mexico"],
            ["Tyrone", "tyrone@example.com", "Senior Response Liaison", "Raynor, Rolfson and Daugherty", "Qatar"],
        ]
    }).render(document.getElementById("table-fixed-header"));

// Hidden Columns
if (document.getElementById("table-hidden-column"))
    new gridjs.Grid({
        columns: [{
            name: 'Name',
            width: '120px',
        }, {
            name: 'Email',
            width: '250px',
        }, {
            name: 'Position',
            width: '250px',
        }, {
            name: 'Company',
            width: '250px',
        },
        {
            name: 'Country',
            hidden: true
        },
        ],
        pagination: {
            limit: 5
        },
        sort: true,
        data: [
            ["Jonathan", "jonathan@example.com", "Senior Implementation Architect", "Hauck Inc", "Holy See"],
            ["Harold", "harold@example.com", "Forward Creative Coordinator", "Metz Inc", "Iran"],
            ["Shannon", "shannon@example.com", "Legacy Functionality Associate", "Zemlak Group", "South Georgia"],
            ["Robert", "robert@example.com", "Product Accounts Technician", "Hoeger", "San Marino"],
            ["Noel", "noel@example.com", "Customer Data Director", "Howell - Rippin", "Germany"],
            ["Traci", "traci@example.com", "Corporate Identity Director", "Koelpin - Goldner", "Vanuatu"],
            ["Kerry", "kerry@example.com", "Lead Applications Associate", "Feeney, Langworth and Tremblay", "Niger"],
            ["Patsy", "patsy@example.com", "Dynamic Assurance Director", "Streich Group", "Niue"],
            ["Cathy", "cathy@example.com", "Customer Data Director", "Ebert, Schamberger and Johnston", "Mexico"],
            ["Tyrone", "tyrone@example.com", "Senior Response Liaison", "Raynor, Rolfson and Daugherty", "Qatar"],
        ]
    }).render(document.getElementById("table-hidden-column"));