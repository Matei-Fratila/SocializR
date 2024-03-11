import { Button, Form } from "react-bootstrap"
import { Controller, SubmitHandler, useForm } from "react-hook-form";
import { CiupercaOption, CiupercaPaginatedResult, Comestibilitate, ComestibilitateOption, Filters, LocDeFructificatie, LocDeFructificatieOption, Luna, LunaOption, MorfologieCorpFructifer, MorfologieCorpFructiferOption, SearchFilters } from "../../types/types";
import Select from "react-select";
import splitCamelCase from "../../helpers/string-helper";
import React from "react";
import mushroomsService from "../../services/mushrooms.service";
import { useSearchParams } from "react-router-dom";

const optionsLocDeFructificatie: LocDeFructificatieOption[] = Object.values(LocDeFructificatie).map(loc => ({ value: loc, label: splitCamelCase(loc) }));
const optionsMorfologieCorpFructifer: MorfologieCorpFructiferOption[] = [
    {
        label: "Lamele",
        value: MorfologieCorpFructifer.HimenoforLamelar
    },
    {
        label: "Tuburi",
        value: MorfologieCorpFructifer.HimenoforTubular
    },
    {
        label: "Nici lamele, nici tuburi",
        value: MorfologieCorpFructifer.HimenoforNelamelarNetubular
    }
]
const optionsComestibilitate: ComestibilitateOption[] = Object.values(Comestibilitate).map(loc => ({ value: loc, label: splitCamelCase(loc) }));
const optionsLuni: LunaOption[] = Object.values(Luna).map((value, index) => ({ label: value, value: index + 1 }));

interface SearchFiltersProps {
    pageIndex: number,
    pageSize: number,
    onFiltered: (mushrooms: CiupercaPaginatedResult, filterParams: any) => void
}

const MushroomSearchFilters = ({pageIndex, pageSize, onFiltered}: SearchFiltersProps) => {
    const [pagingParams, setPagingParams] = useSearchParams();
    const luniDeAparitie = pagingParams.getAll('luniDeAparitie');
    const morfologieCorpFructifer = pagingParams.getAll('morfologieCorpFructifer');
    const locDeFructificatie = pagingParams.getAll('locDeFructificatie');
    const comestibilitate = pagingParams.getAll('comestibilitate');
    const idSpeciiAsemanatoare = pagingParams.getAll('idSpeciiAsemanatoare');
    const esteInSezon = pagingParams.get('esteInSezon');
    const esteMedicinala = pagingParams.get('esteMedicinala');

    console.log(idSpeciiAsemanatoare);

    const [mushroomOptions, setMushroomOptions] = React.useState<CiupercaOption[]>([]);

    const { control, register, handleSubmit, watch, setValue } = useForm<Filters>({
        defaultValues: {
            esteInSezon: esteInSezon === undefined ? undefined : esteInSezon === 'true',
            esteMedicinala: esteMedicinala === undefined ? undefined : esteMedicinala === 'true',
            luniDeAparitie: luniDeAparitie.map((luna) => optionsLuni[Number(luna) - 1]),
            morfologieCorpFructifer: optionsMorfologieCorpFructifer.filter((o) => morfologieCorpFructifer.filter(i => o.value == i).length > 0),
            locDeFructificatie: optionsLocDeFructificatie.filter((o) => locDeFructificatie.filter(i => o.value == i).length > 0),
            comestibilitate: optionsComestibilitate.filter((o) => comestibilitate.filter(i => o.value == i).length > 0),
            idSpeciiAsemanatoare: []
        }
    });

    const watchInfo = watch(["esteInSezon"]);

    const handleSearchMushrooms = async () => {
        try {
            const mushrooms = await mushroomsService.searchMushrooms("");
            const options = mushrooms.map((mushroom) => mushroomsService.mapCiupercaSearchResultToCiupercaOption(mushroom));
            setMushroomOptions(options);
            setValue("idSpeciiAsemanatoare", options.filter((o) => idSpeciiAsemanatoare.filter(i => o.value.toString() === i).length > 0));
        } catch(error) {
            console.log(error);
        }
    }

    React.useEffect(() => {
        handleSearchMushrooms();
    }, [])

    const onSubmit: SubmitHandler<SearchFilters> = async (data) => {
        try {
            const params = {                    
                ...data,
                pageIndex: 0,
                pageSize: pageSize,
                comestibilitate: data.comestibilitate.map((x) => x.value),
                locDeFructificatie: data.locDeFructificatie.map((x) => x.value),
                morfologieCorpFructifer: data.morfologieCorpFructifer.map((x) => x.value),
                luniDeAparitie: data.luniDeAparitie.map((x) => x.value),
                idSpeciiAsemanatoare: data.idSpeciiAsemanatoare.map((x) => x.value)
            }

            const mushrooms: CiupercaPaginatedResult = await mushroomsService.filterMushrooms(params);
            onFiltered(mushrooms, params);
        } catch (error) {
            console.error(error)
        }
    };

    return (
        <Form id="search-filters" onSubmit={handleSubmit(onSubmit)}>
            <h5 className="kaki">Filtre de căutare</h5>
            <Form.Group className="mb-3" controlId="esteInSezon">
                <Form.Check type="switch" label={"Este în sezon"} {...register("esteInSezon")} />
            </Form.Group>

            {!watchInfo[0] &&
                <Form.Group className="mb-3" controlId="luniDeAparitie">
                    <Form.Label>Lunile de apariție</Form.Label>
                    <Controller
                        name="luniDeAparitie"
                        control={control}
                        render={({ field }) => (
                            <Select
                                {...field}
                                options={optionsLuni}
                                isMulti={true}
                            />
                        )}
                    />
                </Form.Group>
            }

            <Form.Group className="mb-3" controlId="morfologieCorpFructifer">
                <Form.Label >Corpul fructifer prezintă</Form.Label>
                <Controller
                    name="morfologieCorpFructifer"
                    control={control}
                    render={({ field }) => (
                        <Select
                            {...field}
                            options={optionsMorfologieCorpFructifer}
                            isMulti={true}
                        />
                    )}
                />
            </Form.Group>

            <Form.Group className="mb-3" controlId="locDeFructificatie">
                <Form.Label>Loc de fructificație</Form.Label>
                <Controller
                    name="locDeFructificatie"
                    control={control}
                    render={({ field }) => (
                        <Select
                            {...field}
                            options={optionsLocDeFructificatie}
                            isMulti={true}
                        />
                    )}
                />
            </Form.Group>

            <Form.Group className="mb-3" controlId="comestibilitate">
                <Form.Label>Comestibilitate</Form.Label>
                <Controller
                    name="comestibilitate"
                    control={control}
                    render={({ field }) => (
                        <Select
                            {...field}
                            options={optionsComestibilitate}
                            isMulti={true}
                        />
                    )}
                />
            </Form.Group>

            <Form.Group className="mb-3" controlId="esteMedicinala">
                <Form.Check type="switch" label={"Are proprietăți curative"} {...register("esteMedicinala")} />
            </Form.Group>

            <Form.Group className="mb-3" controlId="idSpeciiAsemanatoare">
                <Form.Label>Se aseamănă cu:</Form.Label>
                <Controller
                    name="idSpeciiAsemanatoare"
                    control={control}
                    render={({ field }) => (
                        <Select
                            {...field}
                            options={mushroomOptions}
                            isMulti={true}
                        />
                    )}
                />
            </Form.Group>
            <Button type="submit">Căutare</Button>
        </Form>
    );
};

export default MushroomSearchFilters;