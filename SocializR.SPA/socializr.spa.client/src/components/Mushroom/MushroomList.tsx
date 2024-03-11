import Mushroom from "./Mushroom";
import { Col, Container, Row } from "react-bootstrap";
import './Book.css';
import { TablePagination } from "@mui/material";
import { useSearchParams } from "react-router-dom";
import React from "react";
import { Ciuperca, CiupercaPaginatedResult } from "../../types/types";
import mushroomsService from "../../services/mushrooms.service";
import MushroomSearchFilters from "./MushroomSearchFilters";
import Muscarici from "./svgs/Muscarici";

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

    return (
        <Row>
            <Col lg={2} md={4} sm={4} xs={12} /*</Row>*className="sticky-top" style={{"align-self": "flex-start"}}*/>
                <MushroomSearchFilters pageIndex={pageIndex} pageSize={pageSize} onFiltered={onFilterMushrooms}></MushroomSearchFilters>
            </Col>
            <Col lg={10} md={8} sm={8} xs={12}>
                <TablePagination
                    component="div"
                    count={totalCount}
                    page={pageIndex}
                    rowsPerPage={pageSize}
                    onPageChange={handleChangePage}
                    onRowsPerPageChange={handleChangeRowsPerPage}
                    rowsPerPageOptions={[2, 4, 8, 16, 32, { label: 'Toate', value: totalCount }]}
                />
                <Row className="row-cols-1 row-cols-md-2 row-cols-lg-2">
                    {mushrooms?.map((mushroom) => <Col className="mb-4"><Mushroom mushroom={mushroom} key={mushroom.id}></Mushroom></Col>)}
                    
                </Row>
                {mushrooms.length === 0 && <Container className="text-center"><Muscarici></Muscarici></Container>}
                <TablePagination
                    component="div"
                    count={totalCount}
                    page={pageIndex}
                    rowsPerPage={pageSize}
                    onPageChange={handleChangePage}
                    onRowsPerPageChange={handleChangeRowsPerPage}
                    rowsPerPageOptions={[2, 4, 8, 16, 32]}
                />
            </Col>
        </Row>
    );
};

export default MushroomList;