import Post from "./Post";
import { PostsAction, PostsState, Post as PostModel, PostsListProps } from "../types/types";
import React from "react";
import postsService from "../services/posts.service";
import InfiniteScroll from "react-infinite-scroll-component";
import { useParams } from "react-router-dom";

const postsReducer = (
    state: PostsState,
    action: PostsAction
) => {
    switch (action.type) {
        case 'POSTS_FETCH':
            return {
                ...state,
                isLoading: true,
                isError: false,
                data: state.data
            };
        case 'POSTS_FETCH_SUCCESS':
            return {
                ...state,
                isLoading: false,
                isError: false,
                data: [...state.data, ...action.payload]
            };
        case 'POSTS_FETCH_FAILURE':
            return {
                ...state,
                isLoading: false,
                isError: true
            }
        case 'NEW_POST':
            return {
                ...state,
                isLoading: false,
                isError: false,
                data: [action.payload, ...state.data]
            };
        case 'DELETE_POST':
            return {
                ...state,
                isLoading: false,
                isError: false,
                data: state.data.filter(p => p.id !== action.payload)
            };
        case 'INCREASE_PAGE_NUMBER':
            return {
                ...state,
                pageNumber: state.pageNumber + 1
            }
        default:
            throw new Error();
    }
}

const PostList = () => {
    const [posts, dispatchPosts] = React.useReducer(postsReducer, { data: [], pageNumber: 0, isLoading: false, isError: false });
    const [hasMore, setHasMore] = React.useState(true);
    const { id } = useParams();

    const handleFetchPosts = async () => {
        dispatchPosts({ type: 'POSTS_FETCH' });
        try {
            const result = await postsService.getPaginatedAsync(id, posts.pageNumber, true);

            if (posts.pageNumber === 0 && result.data.length === 0) {
                dispatchPosts({
                    type: 'POSTS_FETCH_SUCCESS',
                    payload: []
                });
                setHasMore(false);
            }

            if (result.data.length > 0) {
                dispatchPosts({
                    type: 'POSTS_FETCH_SUCCESS',
                    payload: result.data
                });
                setHasMore(true);
            } else {
                setHasMore(false);
            }

            hasMore && dispatchPosts({
                type: 'INCREASE_PAGE_NUMBER'
            });
        }
        catch {
            dispatchPosts({ type: 'POSTS_FETCH_FAILURE' });
        }
    }

    React.useEffect(() => {
        handleFetchPosts();
    }, []);

    const handleDeletePost = async (id: string) => {
        try {
            await postsService.deletePost(id);
            dispatchPosts({
                type: 'DELETE_POST',
                payload: id
            })
        } catch (e) {

        }
    }

    return (
        <InfiniteScroll
            dataLength={posts.data.length}
            next={handleFetchPosts}
            hasMore={hasMore}
            loader={<p>Loading...</p>}>

            {posts.data.map(post =>
                <Post key={post.id}
                    onRemoveItem={() => handleDeletePost(post.id)}
                    item={post}
                />
            )}

        </InfiniteScroll>
    );
}

export default PostList;