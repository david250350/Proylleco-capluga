function crearDashboardProductos(productos) {
    // Ordenar los productos por precio en orden descendente
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
                    offset: -10,  // Ajustar la posición del texto debajo de la barra
                    rotation: 0  // Asegurar que el texto esté en horizontal
                }
            }
        },
        plugins: [ChartDataLabels]  // Activar el plugin de data labels
    });

    // Aplicar animación de fade-in
    document.getElementById('priceChart').style.opacity = 0;
    setTimeout(function () {
        document.getElementById('priceChart').style.transition = "opacity 1s ease-in-out";
        document.getElementById('priceChart').style.opacity = 1;
    }, 500);
}

function generarColoresDiferentes(num) {
    var colores = [];
    for (var i = 0; i < num; i++) {
        colores.push('hsl(' + (i * 360 / num) + ', 70%, 50%)');
    }
    return colores;
}

function darkenColor(color, percent) {
    var f = color.split(","),
        t = percent < 0 ? 0 : 255,
        p = percent < 0 ? percent * -1 : percent,
        R = parseInt(f[0].slice(4)),
        G = parseInt(f[1]),
        B = parseInt(f[2]);
    return "rgb(" + (Math.round((t - R) * p) + R) + "," + (Math.round((t - G) * p) + G) + "," + (Math.round((t - B) * p) + B) + ")";
}
