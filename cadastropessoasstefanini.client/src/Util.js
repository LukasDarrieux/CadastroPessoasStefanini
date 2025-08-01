export function formatarData(data) {
    if (!data) return "";

    const dataFormatacao = new Date(data);
    const dia = String(dataFormatacao.getDate()).padStart(2, "0");
    const mes = String(dataFormatacao.getMonth() + 1).padStart(2, "0");
    const ano = dataFormatacao.getFullYear();

    const dataFormatada = dia + "/" + mes + "/" + ano;
    return dataFormatada
}

export function formatarCPF(cpf) {
    if (!cpf) return "";

    const numeros = cpf.replace(/\D/g, "");

    return numeros.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, "$1.$2.$3-$4");
}

export function formatarDataAAAAMMDD(data) {
    if (!data) return "";

    const dataFormatacao = new Date(data);
    const dia = String(dataFormatacao.getDate()).padStart(2, "0");
    const mes = String(dataFormatacao.getMonth() + 1).padStart(2, "0");
    const ano = dataFormatacao.getFullYear();

    const dataFormatada = ano + "-" + mes + "-" + dia;
    return dataFormatada
}