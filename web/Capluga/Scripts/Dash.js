function crearDashboardProductos(productos) {
    if (productos.length === 0) {
        console.error("No hay productos disponibles para mostrar en el gráfico.");
        return;
    }

    var labels = productos.map(function (p) { return p.Name; });
    var data = productos.map(function (p) { return p.Price; });

    var ctx = document.getElementById('priceChart').getContext('2d');
    var priceChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Precio de Productos',
                data: data,
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                borderColor: 'rgba(75, 192, 192, 1)',
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}

