import * as yup from "yup";

const updateFloorSchema = yup.object().shape({
  name: yup
    .string()
    .min(3, "Nome não pode ser menor que 3 caractéres.")
    .max(35, "Nome não pode ser maior que 35 caractéres")
});

export { updateFloorSchema };
