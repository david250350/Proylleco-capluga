$$(document).ready(function () {
    var chart = new CanvasJS.Chart("chartContainer", {
        theme: "light2",
        animationEnabled: true,
        title: {
            text: "Margen de Ganancia por Curso"
        },
        axisY: {
            title: "Margen de Ganancia (CRC)",
            includeZero: true
        },
        data: [{
            type: "column",
            dataPoints: margenesGanancia.map(curso => ({
                label: curso.Name,
                y: curso.MargenGanancia
            }))
        }]
    });
    chart.render();
});
