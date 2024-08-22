document.addEventListener("DOMContentLoaded", function () {
    var ctx = document.getElementById('usuariosChart').getContext('2d');

    if (ctx) {
        var totalUsuarios = document.getElementById('totalUsuarios').value;
        var usuariosActivos = document.getElementById('usuariosActivos').value;
        var usuariosInactivos = document.getElementById('usuariosInactivos').value;

        var usuariosChart = new Chart(ctx, {
            type: 'doughnut', // Puedes cambiar esto a 'bar', 'pie', etc., dependiendo de tus preferencias
            data: {
                labels: ['Usuarios Activos', 'Usuarios Inactivos'],
                datasets: [{
                    label: 'Usuarios',
                    data: [usuariosActivos, usuariosInactivos],
                    backgroundColor: [
                        'rgba(75, 192, 192, 0.2)',
                        'rgba(255, 99, 132, 0.2)'
                    ],
                    borderColor: [
                        'rgba(75, 192, 192, 1)',
                        'rgba(255, 99, 132, 1)'
                    ],
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                plugins: {
                    legend: {
                        position: 'top',
                    },
                    tooltip: {
                        callbacks: {
                            label: function (tooltipItem) {
                                var label = tooltipItem.label || '';
                                var value = tooltipItem.raw || 0;
                                var percentage = ((value / totalUsuarios) * 100).toFixed(2);
                                return label + ': ' + value + ' (' + percentage + '%)';
                            }
                        }
                    }
                }
            }
        });
    }
});
