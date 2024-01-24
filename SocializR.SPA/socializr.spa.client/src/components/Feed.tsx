import Post from "./Post";
import { ListProps, PostsAction, PostsListProps, PostsState } from "../types/types";
import React from "react";
import postsService from "../services/posts.service";

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
            };
        case 'POSTS_FETCH_SUCCESS':
            return {
                ...state,
                isLoading: false,
                isError: false,
                data: action.payload
            };
        case 'POSTS_FETCH_FAILURE':
            return {
                ...state,
                isLoading: false,
                isError: true
            }
        default:
            throw new Error();
    }
}

const Feed = () => {
    const [posts, dispatchPosts] = React.useReducer(postsReducer, { data: [], page: 0, isLoading: false, isError: false });

    const handleFetchPosts = React.useCallback(async () => {
        dispatchPosts({ type: 'POSTS_FETCH' });
        try {
            const result = await postsService.getPaginatedAsync(posts.page);
            dispatchPosts({
                type: 'POSTS_FETCH_SUCCESS',
                payload: result.data
            });
        }
        catch {
            dispatchPosts({ type: 'POSTS_FETCH_FAILURE' });
        }
    }, [posts.page]);

    React.useEffect(() => {
        handleFetchPosts();
    }, [handleFetchPosts]);

    return (<>
        {posts.isError && <p>Something went wrong</p>}
        {posts.isLoading ? (<p>Loading...</p>) : (<PostsList posts={posts.data}></PostsList>)}
    </>);
}

const PostsList = ({ posts }: PostsListProps) => (
    <>{posts.map(post => (
        <Post
            key={post.id}
            item={post}
        />
    ))}
    </>
);

export default Feed;