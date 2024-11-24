const validateRegistartionField = (name, value, formData) => {
  let error = "";

  switch (name) {
    case "username": {
      if (!value) {
        error = "Username is required";
      } else if (value.length > 50) {
        error = "Username must be at most 50 characters";
      }
      break;
    }

    case "fullName": {
      if (!value) {
        error = "Full Name is required";
      } else if (value.length > 500) {
        error = "Full Name must be at most 500 characters";
      }
      break;
    }

    case "password": {
      if (!value) {
        error = "Password is required";
      } else if (value.length < 8 || value.length > 16) {
        error = "Password must be between 8 and 16 characters";
      } else if (!/[0-9]/.test(value)) {
        error = "Password must contain at least one digit";
      } else if (!/[A-Z]/.test(value)) {
        error = "Password must contain at least one uppercase letter";
      } else if (!/[!@#$%^&*(),.?":{}|<>]/.test(value)) {
        error = "Password must contain at least one special character";
      }
      break;
    }

    case "confirmPassword":
      if (value !== formData.password) {
        error = "Passwords must match";
      }
      break;

    case "phoneNumber": {
      if (!value) {
        error = "Phone number is required";
      } else if (!/^\+380\d{9}$/.test(value)) {
        error = "Phone number must be in the format +380XXXXXXXXX";
      }
      break;
    }

    case "email": {
      if (!value) {
        error = "Email is required";
      } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(value)) {
        error = "Invalid email address";
      }
      break;
    }

    default: {
      break;
    }
  }

  return error;
};

export { validateRegistartionField };
