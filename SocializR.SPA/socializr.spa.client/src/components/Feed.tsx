import Post from "./Post";
import { PostsAction, PostsListProps, PostsState, Post as PostModel } from "../types/types";
import React from "react";
import postsService from "../services/posts.service";
import PostForm from "./PostForm";

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
        case 'POSTS_NEW_POST':
            return {
                ...state,
                isLoading: false,
                isError: false,
                data: [action.payload, ...state.data]
            };
        case 'POSTS_DELETE_POST':
            return {
                ...state,
                isLoading: false,
                isError: false,
                data: state.data.filter(p => p.id !== action.payload)
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

    const handleNewPost = (post: PostModel) => {
        try {
            dispatchPosts({
                type: 'POSTS_NEW_POST',
                payload: post
            });
        } catch (e) {
            console.error(e);
        }
    }

    React.useEffect(() => {
        handleFetchPosts();
    }, [handleFetchPosts]);

    const handleDeletePost = async (id: string) => {
        try{
            await postsService.deletePost(id);
            dispatchPosts({
                type: 'POSTS_DELETE_POST',
                payload: id
            })
        } catch(e){

        }
    }

    return (<>
        <PostForm onSubmit={handleNewPost}></PostForm>
        {posts.isError && <p>Something went wrong</p>}
        {posts.isLoading ?
            (<p>Loading...</p>)
            : posts.data.map(post => (
                <Post onRemoveItem={() => handleDeletePost(post.id)}
                    key={post.id}
                    item={post}
                />
            ))}
    </>);
}

export default Feed;