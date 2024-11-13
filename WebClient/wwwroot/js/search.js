function addToFavorites(influencerId) {
    // Tạo URL cho yêu cầu API
    var url = 'https://localhost:7290/api/Favourites/add/' + influencerId;

    // Sử dụng fetch API để gửi yêu cầu POST
    fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            // Thêm token JWT nếu cần thiết cho xác thực
            'Authorization': 'Bearer ' + localStorage.getItem('jwtToken')
        }
    })
        .then(response => {
            if (!response.ok) {
                // Kiểm tra nếu response không phải là 200 OK
                throw new Error('Failed to add to favorites. Status: ' + response.status);
            }
            return response.json();  // Chuyển đổi phản hồi thành JSON
        })
        .then(data => {
            console.log('Success:', data); // Log phản hồi từ API

            // Kiểm tra nếu phản hồi trả về thông báo lỗi hoặc thành công
            if (data && data.message === "This influencer is already in your favorites.") {
                alert("This influencer is already in your favorites.");
            } else {
                // Cập nhật giao diện hoặc trạng thái của nút yêu thích
                alert("Added to favorites successfully!");
                var heartIcon = document.querySelector(`.like-kol[data-id='${influencerId}']`);
                heartIcon.classList.add('liked');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            alert('An error occurred. Please try again.');
        });
}
