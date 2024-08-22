document.addEventListener("DOMContentLoaded", function () {
    var ctx = document.getElementById('margenGananciaChart').getContext('2d');

    if (ctx) {
        var labels = JSON.parse(document.getElementById('chartLabels').value);
        var data = JSON.parse(document.getElementById('chartData').value);

        // Función para generar colores traslúcidos aleatorios
        function getRandomColor() {
            var r = Math.floor(Math.random() * 255);
            var g = Math.floor(Math.random() * 255);
            var b = Math.floor(Math.random() * 255);
            return `rgba(${r}, ${g}, ${b}, 0.2)`;
        }

        // Genera un color diferente para cada barra
        var backgroundColors = labels.map(() => getRandomColor());
        var borderColors = backgroundColors.map(color => color.replace('0.2', '1')); // Hacer el borde opaco

        var margenGananciaChart = new Chart(ctx, {
            type: 'bar', // Puedes cambiar esto a 'pie' o 'line' dependiendo de tus preferencias
            data: {
                labels: labels,
                datasets: [{
                    label: 'Margen de Ganancia (20%)',
                    data: data,
                    backgroundColor: backgroundColors,
                    borderColor: borderColors,
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
});