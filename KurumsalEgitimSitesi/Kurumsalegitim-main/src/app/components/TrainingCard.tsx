import { Calendar, Users, Clock, MapPin, Award, Hash } from 'lucide-react';
import { ImageWithFallback } from './figma/ImageWithFallback';

interface TrainingCardProps {
  title: string;
  imageUrl: string;
  hours: string;
  participants: string;
  lessons: string;
  location?: string;
  category?: string;
  level?: string;
}

export function TrainingCard({ 
  title, 
  imageUrl, 
  hours, 
  participants, 
  lessons,
  location,
  category,
  level
}: TrainingCardProps) {
  return (
    <div className="bg-white rounded-lg overflow-hidden shadow-lg hover:shadow-xl transition-shadow">
      <div className="relative h-48">
        <ImageWithFallback 
          src={imageUrl} 
          alt={title}
          className="w-full h-full object-cover"
        />
      </div>
      <div className="p-5">
        <h4 className="text-lg font-semibold text-gray-800 mb-4 h-14 line-clamp-2">{title}</h4>
        <div className="flex flex-wrap gap-4 text-sm text-gray-600 mb-4">
          <div className="flex items-center gap-1">
            <Clock className="w-4 h-4 text-blue-600" />
            <span>{hours}</span>
          </div>
          <div className="flex items-center gap-1">
            <Users className="w-4 h-4 text-blue-600" />
            <span>{participants}</span>
          </div>
          <div className="flex items-center gap-1">
            <Award className="w-4 h-4 text-blue-600" />
            <span>{lessons}</span>
          </div>
        </div>
        <div className="flex flex-wrap gap-2 text-xs">
          {category && (
            <span className="bg-blue-50 text-blue-700 px-3 py-1 rounded-full flex items-center gap-1">
              <Hash className="w-3 h-3" />
              {category}
            </span>
          )}
          {level && (
            <span className="bg-green-50 text-green-700 px-3 py-1 rounded-full flex items-center gap-1">
              <Award className="w-3 h-3" />
              {level}
            </span>
          )}
          {location && (
            <span className="bg-orange-50 text-orange-700 px-3 py-1 rounded-full flex items-center gap-1">
              <MapPin className="w-3 h-3" />
              {location}
            </span>
          )}
        </div>
      </div>
    </div>
  );
}
