import axios, { AxiosResponse } from 'axios';
import { Ciuperca, CiupercaEdit, CiupercaOption, CiupercaPaginatedResult, CiupercaSearchResult, Comestibilitate, Luna, MorfologieCorpFructifer, SearchFilters } from '../types/types';
import splitCamelCase from '../helpers/string-helper';

class MushroomsService {
    mushroomsApi = axios.create({
        baseURL: (!process.env.NODE_ENV || process.env.NODE_ENV === 'development') ? "/mushroomsApi/" : "",
        headers: {
            "Content-Type": "application/json"
        },
        withCredentials: true
    });

    async getMushroom(id: number): Promise<Ciuperca> {
        const axiosResponse: AxiosResponse = await this.mushroomsApi.get(`/Mushrooms/${id}`);
        const mushroom: Ciuperca = axiosResponse.data;
        return mushroom;
    };

    async getPaginatedMushrooms(pageIndex: number, pageSize: number) {
        const axiosResponse: AxiosResponse = await this.mushroomsApi.get(`/Mushrooms/`, { params: { term: "", pageIndex: pageIndex, pageSize: pageSize } });
        const mushrooms: CiupercaPaginatedResult = axiosResponse.data;
        return mushrooms;
    };

    async updateMushroom(ciuperca: Ciuperca) {
        return await this.mushroomsApi.put(`/Mushrooms/`, ciuperca);
    };

    async searchMushrooms(term: string) {
        const axiosResponse: AxiosResponse = await this.mushroomsApi.get(`/Mushrooms/search`, { params: { term: term } });
        const mushrooms: CiupercaSearchResult[] = axiosResponse.data;
        return mushrooms;
    };

    async filterMushrooms(data: any) {
        const axiosResponse: AxiosResponse =
            await this.mushroomsApi.get(`/Mushrooms/filterSearch`, {
                params: data, paramsSerializer: {indexes: null}
            });
        const mushrooms: CiupercaPaginatedResult = axiosResponse.data;
        return mushrooms;
    };

    mapCiupercaToCiupercaEdit(from: Ciuperca): CiupercaEdit {
        const to: CiupercaEdit =
        {
            ...from,
            comestibilitate: { label: splitCamelCase(from.comestibilitate), value: from.comestibilitate },
            locDeFructificatie: from.locDeFructificatie?.map((loc) => ({ label: splitCamelCase(loc), value: loc })),
            morfologieCorpFructifer: { label: splitCamelCase(from.morfologieCorpFructifer), value: from.morfologieCorpFructifer },
            idSpeciiAsemanatoare: from.idSpeciiAsemanatoare?.map((id) => ({ label: id.toString(), value: id })),
            perioadaStart: { label: Object.values(Luna)[from.perioada[0] - 1], value: from.perioada[0] },
            perioadaEnd: { label: Object.values(Luna)[from.perioada[1] - 1], value: from.perioada[1] }
        };

        return to;
    };

    mapCiupercaEditToCiuperca(from: CiupercaEdit): Ciuperca {
        const to: Ciuperca = {
            ...from,
            comestibilitate: from.comestibilitate.value,
            locDeFructificatie: from.locDeFructificatie?.map((loc) => (loc.value)),
            morfologieCorpFructifer: from.morfologieCorpFructifer.value,
            idSpeciiAsemanatoare: from.idSpeciiAsemanatoare?.map((id) => id.value),
            perioada: [from.perioadaStart.value, from.perioadaEnd.value]
        };

        return to;
    };

    mapCiupercaSearchResultToCiupercaOption(ciupercaSearchResult: CiupercaSearchResult): CiupercaOption {
        const option: CiupercaOption = {
            label: ciupercaSearchResult.nume,
            value: ciupercaSearchResult.id
        };

        return option;
    };

    getDefaultCiupercaEdit(): CiupercaEdit {
        return {
            id: 0,
            denumire: "",
            denumirePopulara: "",
            corpulFructifer: "",
            ramurile: "",
            palaria: "",
            piciorul: "",
            stratulHimenial: "",
            gleba: "",
            tuburileSporifere: "",
            lamelele: "",
            carnea: "",
            perioadaDeAparitie: "",
            valoareaAlimentara: "",
            speciiAsemanatoare: "",
            idSpeciiAsemanatoare: [],
            esteMedicinala: false,
            comestibilitate: { value: Comestibilitate.Necomestibila, label: "" },
            locDeFructificatie: [],
            morfologieCorpFructifer: { value: MorfologieCorpFructifer.HimenoforLamelar, label: MorfologieCorpFructifer.HimenoforLamelar },
            luniDeAparitie: [],
            perioadaStart: { label: "", value: 0 },
            perioadaEnd: { label: "", value: 0 }
        }
    }
}

export default new MushroomsService();