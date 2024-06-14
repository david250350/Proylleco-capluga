function cargarUbicaciones() {
    fetch('/Funcion/Ubicaciones.json')
        .then(response => response.json()) // Convertir la respuesta a JSON
        .then(data => {
            const provinciaDropdown = document.getElementById('provincia-dropdown');
            const cantonDropdown = document.getElementById('canton-dropdown');
            const distritoDropdown = document.getElementById('distrito-dropdown');

            // Poblamos el dropdown de provincias
            for (let provinciaId in data.provincias) {
                let provincia = data.provincias[provinciaId];
                let option = new Option(provincia.nombre, provinciaId);
                provinciaDropdown.add(option);
            }

            provinciaDropdown.addEventListener('change', (event) => {
                cantonDropdown.innerHTML = 'Seleccione un cantón'; // Limpiar cantones previos
                distritoDropdown.innerHTML = 'Seleccione un distrito'; // Limpiar distritos previos

                let selectedProvincia = data.provincias[event.target.value];
                for (let cantonId in selectedProvincia.cantones) {
                    let canton = selectedProvincia.cantones[cantonId];
                    let option = new Option(canton.nombre, cantonId);
                    cantonDropdown.add(option);
                }
            });

            cantonDropdown.addEventListener('change', (event) => {
                distritoDropdown.innerHTML = 'Seleccione un distrito'; // Limpiar distritos previos
                let selectedProvincia = data.provincias[provinciaDropdown.value];
                let selectedCanton = selectedProvincia.cantones[event.target.value];

                for (let distritoId in selectedCanton.distritos) {
                    let distrito = selectedCanton.distritos[distritoId];
                    let option = new Option(distrito, distritoId);
                    distritoDropdown.add(option);
                }
            });
        })
        .catch(error => console.error('Error al obtener las ubicaciones:', error));
}

document.addEventListener('DOMContentLoaded', cargarUbicaciones);
