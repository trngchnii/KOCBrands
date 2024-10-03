    document.getElementById("btn-search").addEventListener("click", function () {
        const form = document.getElementById("searchForm");
    const formData = new FormData(form);

    // Chuyển dữ liệu form thành JSON object
    const searchCriteria = { };
        formData.forEach((value, key) => {
        searchCriteria[key] = value;
        });

    // Gửi yêu cầu đến API backend để tìm kiếm
    fetch("/api/SearchKOL", {
        method: "POST",
    headers: {
        "Content-Type": "application/json"
            },
    body: JSON.stringify(searchCriteria)
        })
        .then(response => response.json())
        .then(data => {
            // Cập nhật danh sách KOL với kết quả trả về từ API
            const searchResultDiv = document.getElementById("search-kocs");
            searchResultDiv.innerHTML = data.map(kol => `
    <div class="kol-item">
        <h3>${kol.name}</h3>
        <p>Followers: ${kol.followers}</p>
        <p>Giá Booking: ${kol.bookingPrice}</p>
    </div>
    `).join('');
        });
    });
