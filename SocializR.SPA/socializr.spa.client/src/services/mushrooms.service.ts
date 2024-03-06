import axios, { AxiosResponse } from 'axios';
import { Ciuperca, CiupercaEdit, CiupercaOption, CiupercaSearchResult, Comestibilitate, MorfologieCorpFructifer } from '../types/types';
import splitCamelCase from '../helpers/string-helper';

class MushroomsService {
    mushroomsApi = axios.create({
        baseURL: (!process.env.NODE_ENV || process.env.NODE_ENV === 'development') ? "/mushroomsApi/" : "",
        headers: {
            "Content-Type": "application/json"
        },
        withCredentials: true
    });

    async getMushroom(id: string | undefined): Promise<Ciuperca> {
        const axiosResponse: AxiosResponse = await this.mushroomsApi.get(`/Mushrooms/${id}`);
        const mushroom: Ciuperca = axiosResponse.data;
        return mushroom;
    }

    async updateMushroom(ciuperca: Ciuperca) {
        return await this.mushroomsApi.put(`/Mushrooms/`, ciuperca);
    };

    async searchMushroom(term: string, pageIndex: number, pageSize: number) {
        const axiosResponse: AxiosResponse = await this.mushroomsApi.get(`/Mushrooms/`, {params: {term: term, pageIndex: pageIndex, pageSize: pageSize}});
        const mushrooms: CiupercaSearchResult[] = axiosResponse.data;
        return mushrooms;
    };

    mapCiupercaToCiupercaEdit(ciuperca: Ciuperca): CiupercaEdit {
        const editMushroom: CiupercaEdit =
        {
            ...ciuperca,
            comestibilitate: { label: splitCamelCase(ciuperca.comestibilitate), value: ciuperca.comestibilitate },
            locDeFructificatie: ciuperca.locDeFructificatie.map((loc) => ({ label: splitCamelCase(loc), value: loc })),
            morfologieCorpFructifer: { label: splitCamelCase(ciuperca.morfologieCorpFructifer), value: ciuperca.morfologieCorpFructifer },
            idSpeciiAsemanatoare: ciuperca.idSpeciiAsemanatoare.map((id) => ({ label: id.toString(), value: id}))
        };

        return editMushroom;
    };

    mapCiupercaEditToCiuperca(ciupercaEdit: CiupercaEdit): Ciuperca {
        const ciuperca: Ciuperca = {
            ...ciupercaEdit,
            comestibilitate: ciupercaEdit.comestibilitate.value,
            locDeFructificatie: ciupercaEdit.locDeFructificatie.map((loc) => (loc.value)),
            morfologieCorpFructifer: ciupercaEdit.morfologieCorpFructifer.value,
            idSpeciiAsemanatoare: ciupercaEdit.idSpeciiAsemanatoare.map((id) => id.value)
        };

        return ciuperca;
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
            comestibilitate: { value: Comestibilitate.Necunoscuta, label: Comestibilitate.Necunoscuta },
            locDeFructificatie: [],
            morfologieCorpFructifer: { value: MorfologieCorpFructifer.HimenoforLamelar, label: MorfologieCorpFructifer.HimenoforLamelar },
            luniDeAparitie: [],
            perioada: [],
        }
    }
}

export default new MushroomsService();