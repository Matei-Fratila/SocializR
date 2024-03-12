import Mushroom from "./Mushroom";
import { Col, Container, Row } from "react-bootstrap";
import { TablePagination } from "@mui/material";
import { useSearchParams } from "react-router-dom";
import React from "react";
import { Ciuperca, CiupercaPaginatedResult } from "../../types/types";
import mushroomsService from "../../services/mushrooms.service";
import MushroomSearchFilters from "./MushroomSearchFilters";
import Muscarici from "./svgs/Muscarici";
import './MushroomList.css';

const MushroomList = () => {
    const [searchParams, setSearchParams] = useSearchParams();
    const [pageIndex, setPageIndex] = React.useState(Number(searchParams.get('pageIndex') ?? 0));
    const [pageSize, setPageSize] = React.useState(Number(searchParams.get('pageSize') ?? 2));
    const [mushrooms, setMushrooms] = React.useState<Ciuperca[]>([]);
    const [totalCount, setTotalCount] = React.useState(0);

    async function fetchMushrooms() {
        try {
            var result = await mushroomsService.filterMushrooms(searchParams);
            console.log(result);
            setMushrooms(result.ciuperci);
            setTotalCount(result.totalCount);

        } catch (error) {
            console.error(error);
        }
    }

    React.useEffect(() => {
        searchParams.set("pageIndex", pageIndex.toString());
        searchParams.set("pageSize", pageSize.toString());
        setSearchParams(searchParams);
        fetchMushrooms();
    }, [pageIndex, pageSize]);

    const handleChangePage = (event: React.MouseEvent<HTMLButtonElement> | null, newPage: number) => {
        setPageIndex(newPage);
        window.scrollTo(0, 0);
    };

    const handleChangeRowsPerPage = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const pageSize = event.target.value;
        setPageSize(parseInt(pageSize, 10));
        setPageIndex(0);
        window.scrollTo(0, 0);
    };

    const onFilterMushrooms = (mushrooms: CiupercaPaginatedResult, filterParams: any) => {
        setMushrooms(mushrooms.ciuperci);
        setTotalCount(mushrooms.totalCount);
        setSearchParams({ ...filterParams });
        setPageIndex(0);
    };

    const hasResults = mushrooms.length !== 0;

    return (
        // <Container>
        <Row>
            <Col lg={hasResults ? 2 : 4} md={4} sm={4} xs={12}>
                <Container>
                    <MushroomSearchFilters pageIndex={pageIndex} pageSize={pageSize} onFiltered={onFilterMushrooms}></MushroomSearchFilters>
                </Container>
            </Col>
            <Col lg={hasResults ? 10 : 8} md={8} sm={8} xs={12}>
                <Container>
                    {hasResults &&
                        <Col lg={10} md={8} sm={8} xs={12}>
                            <Container>
                                <TablePagination
                                    component="div"
                                    count={totalCount}
                                    page={pageIndex}
                                    rowsPerPage={pageSize}
                                    onPageChange={handleChangePage}
                                    onRowsPerPageChange={handleChangeRowsPerPage}
                                    rowsPerPageOptions={[2, 4, 8, 16, 32, { label: 'Toate', value: totalCount }]}
                                    labelRowsPerPage="Ciuperci pe pagină"
                                />
                                <Row className="row-cols-1 row-cols-md-1 row-cols-lg-2">
                                    {mushrooms?.map((mushroom) => <Col className="mb-4"><Mushroom mushroom={mushroom} key={mushroom.id}></Mushroom></Col>)}
                                </Row>
                                <TablePagination
                                    component="div"
                                    count={totalCount}
                                    page={pageIndex}
                                    rowsPerPage={pageSize}
                                    onPageChange={handleChangePage}
                                    onRowsPerPageChange={handleChangeRowsPerPage}
                                    rowsPerPageOptions={[2, 4, 8, 16, 32, { label: 'Toate', value: totalCount }]}
                                    labelRowsPerPage="Ciuperci pe pagină"
                                />
                            </Container>
                        </Col>
                    }
                    {!hasResults &&
                        <Row className="row-cols-1 text-center">
                            <Col><Muscarici size={6}></Muscarici></Col>
                            <Col>
                                <div className="border rounded-pill mt-4 p-4">
                                    <span className="fw-bold">Ups!</span> <br />
                                    <span>Nu există nicio ciupercă în baza noastră de date care să îndeplinească aceste criterii. </span> <br />
                                    <span>Încearcă alte criterii de filtrare. </span> <br />
                                </div>
                            </Col>
                        </Row>
                    }
                </Container>
            </Col>
        </Row>
        // </Container>
    );
};

export default MushroomList;