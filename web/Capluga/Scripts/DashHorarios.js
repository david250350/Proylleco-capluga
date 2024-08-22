document.addEventListener("DOMContentLoaded", function () {

    var ctxRosca = document.getElementById('citasPorDiaRoscaChart').getContext('2d');

    if (ctxRosca) {
        var dias = JSON.parse(document.getElementById('dias').value);
        var totalCitasPorDia = JSON.parse(document.getElementById('totalCitasPorDia').value);

        var citasPorDiaRoscaChart = new Chart(ctxRosca, {
            type: 'doughnut',
            data: {
                labels: dias,
                datasets: [{
                    label: 'Citas por Día',
                    data: totalCitasPorDia,
                    backgroundColor: [
                        'rgba(75, 192, 192, 0.2)',
                        'rgba(255, 99, 132, 0.2)',
                        'rgba(255, 206, 86, 0.2)',
                        'rgba(54, 162, 235, 0.2)',
                        'rgba(153, 102, 255, 0.2)',
                        'rgba(255, 159, 64, 0.2)',
                        'rgba(99, 255, 132, 0.2)'
                    ],
                    borderColor: [
                        'rgba(75, 192, 192, 1)',
                        'rgba(255, 99, 132, 1)',
                        'rgba(255, 206, 86, 1)',
                        'rgba(54, 162, 235, 1)',
                        'rgba(153, 102, 255, 1)',
                        'rgba(255, 159, 64, 1)',
                        'rgba(99, 255, 132, 1)'
                    ],
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                plugins: {
                    legend: {
                        position: 'top',
                    }
                }
            }
        });
    }

    // Gráfico de Línea: Tendencia de Citas a lo Largo del Tiempo
    var ctxLinea = document.getElementById('citasTendenciaChart').getContext('2d');

    if (ctxLinea) {
        var fechas = JSON.parse(document.getElementById('fechas').value);
        var totalCitasPorFecha = JSON.parse(document.getElementById('totalCitasPorFecha').value);

        var citasTendenciaChart = new Chart(ctxLinea, {
            type: 'line',
            data: {
                labels: fechas,
                datasets: [{
                    label: 'Número de Citas',
                    data: totalCitasPorFecha,
                    backgroundColor: 'rgba(153, 102, 255, 0.2)',
                    borderColor: 'rgba(153, 102, 255, 1)',
                    borderWidth: 2,
                    fill: true,
                }]
            },
            options: {
                scales: {
                    y: {
                        beginAtZero: true
                    }
                },
                responsive: true,
                plugins: {
                    legend: {
                        display: true,
                        position: 'top',
                    }
                }
            }
        });
    }
});
