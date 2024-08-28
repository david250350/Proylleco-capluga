function crearDashboardProductos(productos) {
    productos.sort((a, b) => b.Price - a.Price);

    var ctx = document.getElementById('priceChart').getContext('2d');
    var colores = generarColoresDiferentes(productos.length);

    var chart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: productos.map(p => p.Name),
            datasets: [{
                label: 'Precio',
                data: productos.map(p => p.Price),
                backgroundColor: colores,
                borderColor: colores.map(c => darkenColor(c, 0.2)),
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            },
            plugins: {
                legend: {
                    display: false
                },
                tooltip: {
                    backgroundColor: 'rgba(0,0,0,0.7)',
                    titleColor: '#fff',
                    bodyColor: '#fff',
                    borderColor: 'rgba(0,0,0,0.1)',
                    borderWidth: 1
                },
                datalabels: {
                    anchor: 'end',
                    align: 'center',
                    formatter: function (value, context) {
                        var total = productos[context.dataIndex].Quantity * value;
                        return total.toLocaleString('es-CR', { style: 'currency', currency: 'CRC' });
                    },
                    color: '#000',
                    font: {
                        weight: 'bold',
                        size: 12
                    },
                    offset: -10,
                    rotation: 0
                }
            }
        },
        plugins: [ChartDataLabels]
    });

    // Eliminar la animación de fade-in para depuración
    // document.getElementById('priceChart').style.opacity = 0;
    // setTimeout(function () {
    //     document.getElementById('priceChart').style.transition = "opacity 1s ease-in-out";
    //     document.getElementById('priceChart').style.opacity = 1;
    // }, 500);
}
