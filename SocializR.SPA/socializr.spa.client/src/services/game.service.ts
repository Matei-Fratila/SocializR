import { AxiosResponse } from 'axios';
import axiosInstance from '../helpers/axios-helper';
import { Difficulty, EndGameModel, StartGameModel } from '../types/types';

class GameService {
    async startGame(): Promise<StartGameModel> {
        const response: AxiosResponse = await axiosInstance.get('/Game/start');
        const game: StartGameModel = response.data;
        return game;
    };

    async submitAnswer(difficulty: Difficulty, isCorrect: boolean, score: number): Promise<number> {
        try {
            const response: AxiosResponse = await axiosInstance.post('/Game/submit-answer', { difficulty: difficulty, isCorrect: isCorrect, score: score });
            const hearts: number = response.data;
            return hearts;
        } catch (err) {
            console.log(err);
            return 0;
        }
    };

    async endGame(gameInfo: EndGameModel) {
        return await axiosInstance.post(`/Game/end`, gameInfo);
    };
};

export default new GameService();