// BAR CHART
const bar = document.getElementById("barChart");

new Chart(bar, {
    type: "bar",
    data: {
        labels: ["A", "B", "C", "D", "E"],
        datasets: [
            {
                data: [9, 3, 12, 8, 5],
                backgroundColor: "#004d26"
            },
            {
                data: [4, 7, 14, 5, 7],
                backgroundColor: "#66cc7f"
            }
        ]
    },
    options: {
        plugins: {
            legend: { display: false }
        },
        scales: { y: { beginAtZero: true } }
    }
});

// PIE CHART
const pie = document.getElementById("pieChart");

new Chart(pie, {
    type: "pie",
    data: {
        labels: ["3", "6"],
        datasets: [{
            data: [3, 6],
            backgroundColor: ["#66cc7f", "#003d1f"]
        }]
    },
    options: {
        plugins: {
            legend: {
                position: "top",
                labels: {
                    generateLabels: () => [
                        { text: "Series 1", fillStyle: "#66cc7f" },
                        { text: "Series 3", fillStyle: "#003d1f" }
                    ]
                }
            }
        }
    }
});
