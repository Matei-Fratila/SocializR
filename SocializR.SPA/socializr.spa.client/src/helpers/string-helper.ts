const splitCamelCase = (value: string) => value.replace(/([a-z0-9])([A-ZȘ])/g, '$1 $2');

export default splitCamelCase;