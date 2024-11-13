<script>
    $('#filterForm').submit(function (event) {
        event.preventDefault(); // Ngăn chặn việc tải lại trang

    var formData = $(this).serialize(); // Lấy dữ liệu từ form

    $.ajax({
        url: '/Search/Search', // URL của controller
    type: 'POST',
    data: formData, // Dữ liệu lọc
    success: function (response) {
        // Cập nhật kết quả tìm kiếm
        $('#search-kocs').html(response);
              }
          });
      });
</script>