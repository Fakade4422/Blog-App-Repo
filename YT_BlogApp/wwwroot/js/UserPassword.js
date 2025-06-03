document.querySelector("#btn").addEventListener('click', function () {
    // Get the values of the password fields
    
    var password = document.getElementById('createPassword').value;
    var confirmPassword = document.getElementById('confirmPassword').value;

    // Check if the passwords match
    if (password === confirmPassword) {
        console.log("Match");
        ////code below takes the password and stores it in this element, for the password
        document.getElementById('txtPassword').value = password;
    }
    else {
        console.log("Doesn't Match");
    }
});