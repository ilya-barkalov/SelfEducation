
const POST = (url, data, options = defaultPostOptions = {}) => {
    const promise = () => {
        return new Promise((resolve, reject) => {
            $.ajax({
                url,
                method: 'POST',
                data: data,
                ...options,
                success: function (response) {
                    resolve(response);
                },
                error: function (error) {
                    reject(error);
                }
            });
        });
    };

    return promise();
}

const GET = (url, data, options = { dataType: 'html' }) => {
    const promise = () => {
        return new Promise((resolve, reject) => {
            $.ajax({
                url,
                method: 'GET',
                data: data,
                ...options,
                success: function (response) {
                    resolve(response);
                },
                error: function (error) {
                    reject(error);
                }
            });
        });
    };

    return promise();
};